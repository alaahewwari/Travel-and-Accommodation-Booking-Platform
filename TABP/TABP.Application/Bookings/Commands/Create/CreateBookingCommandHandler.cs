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
            var existingUser = await userRepository.GetUserByIdAsync(userContext.UserId, cancellationToken);
            if (existingUser is null)
            {
                return Result<BookingCreationResponse>.Failure(UserErrors.UserNotFound);
            }
            var existingHotel = await hotelRepository.GetHotelByIdAsync(request.HotelId, cancellationToken);
            if (existingHotel is null)
            {
                return Result<BookingCreationResponse>.Failure(HotelErrors.HotelNotFound);
            }
            var rooms = await roomRepository.GetRoomsByIdsAsync(request.RoomIds, request.HotelId, cancellationToken);
            if (rooms.Count() != request.RoomIds.Count)
            {
                return Result<BookingCreationResponse>.Failure(RoomErrors.RoomsNotFound);
            }
            if (request.CheckInDate >= request.CheckOutDate)
            {
                return Result<BookingCreationResponse>.Failure(BookingErrors.InvalidBookingDates);
            }
            var createdBooking = new Booking();
            await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
            {
                var hasOverlap = await bookingRepository.CheckBookingOverlapAsync(request.RoomIds, request.CheckInDate, request.CheckOutDate, cancellationToken);
                if (hasOverlap)
                {
                    throw new InvalidOperationException("One or more rooms are already booked for the selected dates.");
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
                    //Rooms = rooms.ToList(),
                    UserId = userContext.UserId,
                    HotelId = request.HotelId,
                };
                createdBooking = await bookingRepository.CreateAsync(booking, cancellationToken);
                var invoice = new Invoice
                {
                    IssueDate = DateTime.UtcNow,
                    TotalAmount = totalPrice,
                    Status = PaymentStatus.Pending,
                    InvoiceNumber = $"INV-{Guid.NewGuid():N}"[..10]
                };
                booking.Invoice = invoice;
                invoice.Booking = booking;
            }, cancellationToken);
            var roomNumbers = rooms.Select(r => r.Number).ToList();
            await ProcessPostBookingOperationsAsync(createdBooking, existingHotel, existingUser, cancellationToken);
            return Result<BookingCreationResponse>.Success(createdBooking.ToBookingCreationResponse(existingHotel, roomNumbers));
        }
        private async Task ProcessPostBookingOperationsAsync(
           Booking createdBooking,
           Hotel hotel,
           User user,
           CancellationToken cancellationToken)
        {
            try
            {
                var htmlInvoice = await invoiceTemplate.BuildHtmlAsync(createdBooking, hotel, user);
                var pdfBytes = await pdfService.GenerateBookingInvoicePdf(htmlInvoice);
                using var invoiceAttachment = new Attachment(
                    new MemoryStream(pdfBytes),
                    $"Invoice-{createdBooking.Invoice!.InvoiceNumber}.pdf",
                    MediaTypeNames.Application.Pdf);
                var emailMessage = messageBuilder.BuildBookingConfirmation(invoiceAttachment, user);
                await emailService.SendEmailAsync(emailMessage);
            }
            catch (Exception ex)
            {
                // Email failure doesn't affect booking success
                // Consider implementing a retry mechanism or background job
            }
        }
    }
}