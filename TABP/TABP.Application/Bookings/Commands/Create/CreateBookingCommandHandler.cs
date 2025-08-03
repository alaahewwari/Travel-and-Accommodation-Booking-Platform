using MediatR;
using System.Net.Mail;
using System.Net.Mime;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Mapper;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Rooms.Common;
using TABP.Application.Users.Common.Errors;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Services.Booking;
namespace TABP.Application.Bookings.Commands.Create
{
    public class CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IUserContext userContext,
        IUserRepository userRepository,
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IEmailMessageBuilder messageBuilder,
        IPdfService pdfService,
        IInvoiceTemplateBuilder invoiceTemplate,
        IPricingService pricingService
        ) : IRequestHandler<CreateBookingCommand, Result<BookingCreationResponse>>
    {
        public async Task<Result<BookingCreationResponse>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await ValidateUserExistsAsync(cancellationToken);
                var existingHotel = await ValidateHotelExistsAsync(request.HotelId, cancellationToken);
                var rooms = await ValidateRoomsExistAsync(request.RoomIds, request.HotelId, cancellationToken);
                ValidateBookingDates(request.CheckInDate, request.CheckOutDate);
                var createdBooking = await CreateBookingInTransactionAsync(request, rooms, existingHotel, existingUser, cancellationToken);
                var roomNumbers = rooms.Select(r => r.Number).ToList();
                return Result<BookingCreationResponse>.Success(createdBooking.ToBookingCreationResponse(existingHotel, roomNumbers));
            }
            catch (EntityNotFoundException ex)
            {
                return ex.EntityType switch
                {
                    "User" => Result<BookingCreationResponse>.Failure(UserErrors.UserNotFound),
                    "Hotel" => Result<BookingCreationResponse>.Failure(HotelErrors.HotelNotFound),
                    "Rooms" => Result<BookingCreationResponse>.Failure(RoomErrors.RoomNotFound),
                    _ => Result<BookingCreationResponse>.Failure(BookingErrors.UnexpectedError)
                };
            }
            catch (InvalidBookingDatesException)
            {
                return Result<BookingCreationResponse>.Failure(BookingErrors.InvalidBookingDates);
            }
            catch (BookingOverlapException)
            {
                return Result<BookingCreationResponse>.Failure(BookingErrors.BookingOverlap);
            }
            catch (BookingCreationException)
            {
                return Result<BookingCreationResponse>.Failure(BookingErrors.BookingCreationFailed);
            }
            catch (Exception)
            {
                return Result<BookingCreationResponse>.Failure(BookingErrors.UnexpectedError);
            }
        }
        private async Task<User> ValidateUserExistsAsync(CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByIdAsync(userContext.UserId, cancellationToken);
            if (existingUser is null)
            {
                throw new EntityNotFoundException("User", userContext.UserId);
            }
            return existingUser;
        }
        private async Task<Hotel> ValidateHotelExistsAsync(long hotelId, CancellationToken cancellationToken)
        {
            var existingHotel = await hotelRepository.GetHotelByIdAsync(hotelId, cancellationToken);
            if (existingHotel is null)
            {
                throw new EntityNotFoundException("Hotel", hotelId);
            }
            return existingHotel;
        }
        private async Task<IEnumerable<Room>> ValidateRoomsExistAsync(IEnumerable<long> roomIds, long hotelId, CancellationToken cancellationToken)
        {
            var rooms = await roomRepository.GetRoomsByIdsAsync(roomIds, hotelId, cancellationToken);
            if (rooms.Count() != roomIds.Count())
            {
                var foundRoomIds = rooms.Select(r => r.Id);
                var missingRoomIds = roomIds.Except(foundRoomIds);
                throw new EntityNotFoundException("Rooms", string.Join(", ", missingRoomIds));
            }
            return rooms;
        }
        private static void ValidateBookingDates(DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkInDate >= checkOutDate || checkInDate < DateTime.UtcNow)
            {
                throw new InvalidBookingDatesException(checkInDate, checkOutDate);
            }
        }
        private async Task<Booking> CreateBookingInTransactionAsync(
            CreateBookingCommand request,
            IEnumerable<Room> rooms,
            Hotel hotel,
            User user,
            CancellationToken cancellationToken)
        {
            Booking createdBooking = null!;
            try
            {
                await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
                {
                    var hasOverlap = await bookingRepository.CheckBookingOverlapAsync(
                        request.RoomIds,
                        request.CheckInDate,
                        request.CheckOutDate,
                        cancellationToken);
                    if (hasOverlap)
                    {
                        throw new BookingOverlapException(request.RoomIds);
                    }
                    var nights = (request.CheckOutDate - request.CheckInDate).Days;
                    var totalPrice = pricingService.CalculateTotalPrice(rooms, nights);
                    var booking = new Booking
                    {
                        PaymentMethod = request.PaymentMethod,
                        CheckInDate = request.CheckInDate,
                        CheckOutDate = request.CheckOutDate,
                        GuestRemarks = request.GuestRemarks,
                        TotalPrice = totalPrice,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Status = BookingStatus.Pending,
                        Rooms = rooms.ToList(),
                        UserId = userContext.UserId,
                        HotelId = request.HotelId,
                    };
                    createdBooking = await bookingRepository.CreateAsync(booking, cancellationToken);
                    var invoice = new Invoice
                    {
                        IssueDate = DateTime.UtcNow,
                        TotalAmount = totalPrice,
                        Status = PaymentStatus.Pending,
                        InvoiceNumber = GenerateInvoiceNumber()
                    };
                    booking.Invoice = invoice;
                    invoice.Booking = booking;
                    await ProcessPostBookingOperationsAsync(createdBooking, hotel, user, cancellationToken);
                }, cancellationToken);
            }
            catch (BookingOverlapException)
            {
                throw;
            }
            catch (PostBookingOperationsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BookingCreationException("Failed to create booking within transaction.", ex);
            }
            return createdBooking;
        }
        private static string GenerateInvoiceNumber()
        {
            return $"INV-{Guid.NewGuid():N}"[..10];
        }
        private async Task ProcessPostBookingOperationsAsync(
           Booking createdBooking,
           Hotel hotel,
           User user,
           CancellationToken cancellationToken)
        {
            try
            {
                var htmlInvoice = await GenerateInvoiceHtmlAsync(createdBooking, hotel, user);
                var pdfBytes = await GenerateInvoicePdfAsync(htmlInvoice);
                await SendBookingConfirmationEmailAsync(createdBooking, pdfBytes, user);
            }
            catch (Exception ex)
            {
                throw new PostBookingOperationsException("Email and invoice generation", ex);
            }
        }
        private async Task<string> GenerateInvoiceHtmlAsync(Booking booking, Hotel hotel, User user)
        {
            try
            {
                return await invoiceTemplate.BuildHtmlAsync(booking, hotel, user);
            }
            catch (Exception ex)
            {
                throw new PostBookingOperationsException("Invoice HTML generation", ex);
            }
        }
        private async Task<byte[]> GenerateInvoicePdfAsync(string htmlInvoice)
        {
            try
            {
                return await pdfService.GenerateBookingInvoicePdf(htmlInvoice);
            }
            catch (Exception ex)
            {
                throw new PostBookingOperationsException("PDF generation", ex);
            }
        }
        private async Task SendBookingConfirmationEmailAsync(Booking booking, byte[] pdfBytes, User user)
        {
            try
            {
                using var invoiceAttachment = new Attachment(
                    new MemoryStream(pdfBytes),
                    $"Invoice-{booking.Invoice!.InvoiceNumber}.pdf",
                    MediaTypeNames.Application.Pdf);
                var emailMessage = messageBuilder.BuildBookingConfirmation(invoiceAttachment, user);
                await emailService.SendEmailAsync(emailMessage);
            }
            catch (Exception ex)
            {
                throw new PostBookingOperationsException("Email sending", ex);
            }
        }
    }
}