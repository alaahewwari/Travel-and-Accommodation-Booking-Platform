using AutoFixture;
using FluentAssertions;
using Moq;
using System.Net.Mail;
using TABP.Application.Bookings.Commands.Create;
using TABP.Application.Bookings.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Rooms.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Email;
using TABP.Domain.Models.Payment;
using TABP.Domain.Services.Booking;
namespace TABP.Application.Tests.Bookings
{
    public class CreateBookingTests
    {
        private readonly Mock<IBookingRepository> _bookingRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IUserContext> _userContextMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IHotelRepository> _hotelRepoMock = new();
        private readonly Mock<IRoomRepository> _roomRepoMock = new();
        private readonly Mock<IEmailMessageBuilder> _messageBuilderMock = new();
        private readonly Mock<IPdfService> _pdfServiceMock = new();
        private readonly Mock<IInvoiceTemplateBuilder> _invoiceTemplateMock = new();
        private readonly Mock<IPricingService> _pricingServiceMock = new();
        private readonly Mock<IPaymentService> _paymentServiceMock = new();
        private readonly Mock<IPaymentRepository> _paymentRepoMock = new();
        private readonly CreateBookingCommandHandler _handler;
        private readonly IFixture _fixture;

        public CreateBookingTests()
        {
            _userContextMock.Setup(x => x.UserId).Returns(1);
            _handler = new CreateBookingCommandHandler(
                _bookingRepoMock.Object,
                _unitOfWorkMock.Object,
                _emailServiceMock.Object,
                _userContextMock.Object,
                _userRepoMock.Object,
                _hotelRepoMock.Object,
                _roomRepoMock.Object,
                _messageBuilderMock.Object,
                _pdfServiceMock.Object,
                _invoiceTemplateMock.Object,
                _pricingServiceMock.Object,
                _paymentServiceMock.Object,
                _paymentRepoMock.Object
            );
            _fixture = new Fixture();
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Handle_HotelNotFound_ShouldReturnFailure()
        {
            var command = _fixture.Create<CreateBookingCommand>();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<User>());
            _hotelRepoMock.Setup(r => r.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Hotel?)null);
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(HotelErrors.HotelNotFound.Code);
        }

        [Fact]
        public async Task Handle_SomeRoomsNotFound_ShouldReturnFailure()
        {
            var requestedRoomIds = new List<long> { 1, 2, 3 };
            var foundRooms = _fixture.CreateMany<Room>(2).ToList();
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.RoomIds, requestedRoomIds)
                .Create();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<User>());
            _hotelRepoMock.Setup(r => r.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<Hotel>());
            _roomRepoMock.Setup(r => r.GetRoomsByIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(foundRooms);
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(RoomErrors.RoomNotFound.Code);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnSuccess()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var roomIds = rooms.Select(r => r.Id).ToList();
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today.AddDays(1))
                .With(c => c.CheckOutDate, DateTime.Today.AddDays(2))
                .With(c => c.RoomIds, roomIds)
                .Create();
            var user = _fixture.Create<User>();
            var hotel = _fixture.Create<Hotel>();
            var totalPrice = 100m;
            var booking = _fixture.Build<Booking>().With(b => b.Invoice, new Invoice()).Create();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _hotelRepoMock.Setup(r => r.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(hotel);
            _roomRepoMock.Setup(r => r.GetRoomsByIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(rooms);
            _bookingRepoMock.Setup(r => r.CheckBookingOverlapAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _pricingServiceMock.Setup(p => p.CalculateTotalPrice(It.IsAny<IEnumerable<Room>>(), It.IsAny<int>()))
                .Returns(totalPrice);
            _bookingRepoMock.Setup(r => r.CreateAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            _unitOfWorkMock.Setup(u => u.ExecuteResilientTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<CancellationToken, Task>, CancellationToken>((func, ct) => func(ct));
            _invoiceTemplateMock.Setup(x => x.BuildHtmlAsync(It.IsAny<Booking>(), It.IsAny<Hotel>(), It.IsAny<User>()))
                .ReturnsAsync("<html>invoice</html>");
            _pdfServiceMock.Setup(x => x.GenerateBookingInvoicePdf(It.IsAny<string>()))
                .ReturnsAsync(new byte[10]);
            _paymentServiceMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(PaymentResult.Success("pi_test_success"));
            _messageBuilderMock.Setup(x => x.BuildBookingConfirmation(It.IsAny<Attachment>(), It.IsAny<User>()))
                .Returns(new EmailMessage());
            var result = await _handler.Handle(command, default);
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_UserNotFound_ShouldReturnFailure()
        {
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);
            var command = _fixture.Create<CreateBookingCommand>();
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Login.UserNotFound");
        }

        [Fact]
        public async Task Handle_InvalidBookingDates_ShouldReturnFailure()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var roomIds = rooms.Select(r => r.Id).ToList();
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today.AddDays(2))
                .With(c => c.CheckOutDate, DateTime.Today)
                .With(c => c.RoomIds, roomIds)
                .Create();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<User>());
            _hotelRepoMock.Setup(r => r.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<Hotel>());
            _roomRepoMock.Setup(r => r.GetRoomsByIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.CreateMany<Room>(2));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.InvalidBookingDates.Code);
        }

        [Fact]
        public async Task Handle_Overlap_ShouldReturnFailure()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var roomIds = rooms.Select(r => r.Id).ToList();
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today.AddDays(1))
                .With(c => c.CheckOutDate, DateTime.Today.AddDays(2))
                .With(c => c.RoomIds, roomIds)
                .Create();
            _userRepoMock.Setup(x => x.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<User>());
            _hotelRepoMock.Setup(x => x.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<Hotel>());
            _roomRepoMock.Setup(x => x.GetRoomsByIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.CreateMany<Room>(2));
            _bookingRepoMock.Setup(x => x.CheckBookingOverlapAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _unitOfWorkMock.Setup(x => x.ExecuteResilientTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<CancellationToken, Task>, CancellationToken>((func, ct) => func(ct));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Booking.Overlap");
        }

        [Fact]
        public async Task Handle_WhenCreateAsyncThrows_ShouldReturnBookingCreationFailed()
        {
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today.AddDays(1))
                .With(c => c.CheckOutDate, DateTime.Today.AddDays(2))
                .With(c => c.RoomIds, new List<long> { 1, 2 })
                .Create();
            var user = _fixture.Create<User>();
            var hotel = _fixture.Create<Hotel>();
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            _userRepoMock.Setup(x => x.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _hotelRepoMock.Setup(x => x.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(hotel);
            _roomRepoMock.Setup(x => x.GetRoomsByIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(rooms);
            _bookingRepoMock.Setup(x => x.CheckBookingOverlapAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _pricingServiceMock.Setup(x => x.CalculateTotalPrice(It.IsAny<IEnumerable<Room>>(), It.IsAny<int>()))
                .Returns(200m);
            _bookingRepoMock.Setup(x => x.CreateAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Unexpected DB failure"));
            _unitOfWorkMock.Setup(x => x.ExecuteResilientTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<CancellationToken, Task>, CancellationToken>((func, ct) => func(ct));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.BookingCreationFailed.Code);
        }

        [Fact]
        public async Task Handle_InvoiceHtmlGenerationFails_ShouldReturnFailure()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var command = SetupValidCommand(rooms);
            SetupValidMocks(rooms);
            _paymentServiceMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(PaymentResult.Success("pi_test_success"));
            _invoiceTemplateMock.Setup(x => x.BuildHtmlAsync(It.IsAny<Booking>(), It.IsAny<Hotel>(), It.IsAny<User>()))
                .ThrowsAsync(new Exception("HTML generation failed"));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.BookingCreationFailed.Code);
        }

        [Fact]
        public async Task Handle_PdfGenerationFails_ShouldReturnFailure()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var command = SetupValidCommand(rooms);
            SetupValidMocks(rooms);
            _paymentServiceMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(PaymentResult.Success("pi_test_success"));
            _invoiceTemplateMock.Setup(x => x.BuildHtmlAsync(It.IsAny<Booking>(), It.IsAny<Hotel>(), It.IsAny<User>()))
                .ReturnsAsync("<html>invoice</html>");
            _pdfServiceMock.Setup(x => x.GenerateBookingInvoicePdf(It.IsAny<string>()))
                .ThrowsAsync(new Exception("PDF generation failed"));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.BookingCreationFailed.Code);
        }

        [Fact]
        public async Task Handle_EmailSendingFails_ShouldReturnFailure()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var command = SetupValidCommand(rooms);
            SetupValidMocks(rooms);
            _paymentServiceMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(PaymentResult.Success("pi_test_success"));
            _emailServiceMock.Setup(x => x.SendEmailAsync(It.IsAny<EmailMessage>()))
                .ThrowsAsync(new Exception("Email sending failed"));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.BookingCreationFailed.Code);
        }

        [Fact]
        public async Task Handle_UnexpectedError_ShouldReturnUnexpectedError()
        {
            var command = _fixture.Create<CreateBookingCommand>();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Unexpected error"));
            var result = await _handler.Handle(command, default);
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.UnexpectedError.Code);
        }

        [Fact]
        public async Task Handle_SuccessfulPayment_ShouldReturnSuccessWithConfirmedBooking()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var command = SetupValidCommand(rooms);
            SetupValidMocks(rooms);
            var successfulPaymentResult = PaymentResult.Success("pi_test_success");
            _paymentServiceMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(successfulPaymentResult);
            var result = await _handler.Handle(command, default);
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.PaymentInfo.Should().BeNull();
            _paymentServiceMock.Verify(x => x.ProcessPaymentAsync(It.Is<PaymentRequest>(pr =>
                pr.PaymentMethodId == command.PaymentMethodId &&
                pr.Currency == PriceCurrency.USD)), Times.Once);
            _paymentRepoMock.Verify(x => x.CreateAsync(It.Is<Payment>(p =>
                p.PaymentIntentId == "pi_test_success" &&
                p.Status == PaymentStatus.Succeeded &&
                p.ProcessedAt != null), It.IsAny<CancellationToken>()), Times.Once);
            _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<EmailMessage>()), Times.Once);
        }

        private CreateBookingCommand SetupValidCommand(List<Room> rooms)
        {
            return _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today.AddDays(1))
                .With(c => c.CheckOutDate, DateTime.Today.AddDays(3))
                .With(c => c.RoomIds, rooms.Select(r => r.Id).ToList())
                .Create();
        }

        private void SetupValidMocks(List<Room> rooms)
        {
            var user = _fixture.Create<User>();
            var hotel = _fixture.Create<Hotel>();
            var booking = _fixture.Build<Booking>().With(b => b.Invoice, new Invoice()).Create();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _hotelRepoMock.Setup(r => r.GetHotelByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(hotel);
            _roomRepoMock.Setup(r => r.GetRoomsByIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(rooms);
            _bookingRepoMock.Setup(r => r.CheckBookingOverlapAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _pricingServiceMock.Setup(p => p.CalculateTotalPrice(It.IsAny<IEnumerable<Room>>(), It.IsAny<int>()))
                .Returns(200m);
            _bookingRepoMock.Setup(r => r.CreateAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            _unitOfWorkMock.Setup(u => u.ExecuteResilientTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<CancellationToken, Task>, CancellationToken>((func, ct) => func(ct));
            _invoiceTemplateMock.Setup(x => x.BuildHtmlAsync(It.IsAny<Booking>(), It.IsAny<Hotel>(), It.IsAny<User>()))
                .ReturnsAsync("<html>invoice</html>");
            _pdfServiceMock.Setup(x => x.GenerateBookingInvoicePdf(It.IsAny<string>()))
                .ReturnsAsync(new byte[10]);
            _messageBuilderMock.Setup(x => x.BuildBookingConfirmation(It.IsAny<Attachment>(), It.IsAny<User>()))
                .Returns(new EmailMessage());
        }
    }
}