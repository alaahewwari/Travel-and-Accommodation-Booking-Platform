using AutoFixture;
using FluentAssertions;
using Moq;
using System.Net.Mail;
using TABP.Application.Bookings.Commands.Create;
using TABP.Application.Bookings.Common;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Email;
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
                _pricingServiceMock.Object
            );
            _fixture = new Fixture();
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnSuccess()
        {
            var rooms = _fixture.CreateMany<Room>(2).ToList();
            var roomIds = rooms.Select(r => r.Id).ToList();
            // Arrange
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today)
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
            _messageBuilderMock.Setup(x => x.BuildBookingConfirmation(It.IsAny<Attachment>(), It.IsAny<User>()))
                .Returns(new EmailMessage());
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
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
            // Arrange
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
            // Arrange
            var command = _fixture.Build<CreateBookingCommand>()
                .With(c => c.CheckInDate, DateTime.Today)
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
    }
}