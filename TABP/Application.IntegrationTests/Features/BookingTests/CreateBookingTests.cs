using Application.IntegrationTests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TABP.Application.Bookings.Commands.Create;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Services;
using TABP.Persistence.Context;
using static Application.IntegrationTests.Common.IntegrationTestWebAppFactory;
namespace Application.IntegrationTests.Features.BookingTests
{
    public class CreateBookingTests : BaseIntegrationTest
    {
        private readonly ApplicationDbContext _context;
        private readonly TestUserContext _testUserContext;
        private readonly TestDataSeeder _seeder;
        private readonly TestEmailService _emailService;
        private readonly TestPdfService _pdfService;
        public CreateBookingTests(IntegrationTestWebAppFactory factory)
          : base(factory)
        {
            _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _testUserContext = (TestUserContext)_scope.ServiceProvider.GetRequiredService<IUserContext>();
            _seeder = new TestDataSeeder(_context);
            _emailService = (TestEmailService)_scope.ServiceProvider.GetRequiredService<IEmailService>();
            _pdfService = (TestPdfService)_scope.ServiceProvider.GetRequiredService<IPdfService>();
        }
        [Fact]
        public async Task CreateBooking_ShouldSucceed_WithSingleRoom()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            // 2. Set user context
            _testUserContext.SetUser(user.Id, user.Email);
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList());
            // Act
            var result = await Sender.Send(command);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            await VerifyBookingAsync(result.Value.Id, user.Id, 1);
        }
        [Fact]
        public async Task CreateBooking_ShouldSucceed_WithMultipleRooms()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(3);
            var user = await _seeder.SeedUserAsync();
            // 2. Set user context
            _testUserContext.SetUser(user.Id, user.Email);
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList());
            // Act
            var result = await Sender.Send(command);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            await VerifyBookingAsync(result.Value.Id, user.Id, 3);
        }
        [Fact]
        public async Task CreateBooking_ShouldCreateInvoice()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            // 2. Set user context
            _testUserContext.SetUser(user.Id, user.Email);
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList());
            // Act
            var result = await Sender.Send(command);
            Console.WriteLine(result.Error.Description);
            // Assert
            Assert.True(result.IsSuccess);
            var booking = await GetBookingAsync(result.Value.Id);
            Assert.NotNull(booking.Invoice);
            Assert.StartsWith("INV-", booking.Invoice.InvoiceNumber);
            Assert.Equal(PaymentStatus.Pending, booking.Invoice.Status);
            Assert.Equal(booking.TotalPrice, booking.Invoice.TotalAmount);
        }
        [Fact]
        public async Task CreateBooking_ShouldSendConfirmationEmail_AndGenerateInvoicePdf()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            // 2. Set user context
            _testUserContext.SetUser(user.Id, user.Email);
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList());
            // Act
            var result = await Sender.Send(command);
            var sentEmails = _emailService.SentEmails;
            var generatedPdfs = _pdfService.GeneratedPdfs;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            await VerifyBookingAsync(result.Value.Id, user.Id, 1);
            Assert.Single(sentEmails);
            Assert.Equal("Booking Confirmation", sentEmails[0].Subject);
            Assert.Contains(user.Email, sentEmails[0].To);
            Assert.Single(generatedPdfs);
            Assert.NotEmpty(generatedPdfs[0]);
        }
        private CreateBookingCommand CreateBookingCommand(long hotelId, List<long> roomIds)
        {
            return new CreateBookingCommand(
                RoomIds: roomIds,
                HotelId: hotelId,
                CheckInDate: DateTime.Today.AddDays(1),
                CheckOutDate: DateTime.Today.AddDays(4),
                GuestRemarks: "Integration test booking",
                PaymentMethod: PaymentMethod.CreditCard
            );
        }
        private async Task<Booking> GetBookingAsync(long bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Invoice)
                .Include(b => b.Rooms)
                .FirstAsync(b => b.Id == bookingId);
        }
        private async Task VerifyBookingAsync(long bookingId, long expectedUserId, int expectedRoomCount)
        {
            var booking = await GetBookingAsync(bookingId);
            Assert.NotNull(booking);
            Assert.Equal(BookingStatus.Pending, booking.Status);
            Assert.Equal(expectedRoomCount, booking.Rooms.Count);
            Assert.Equal(expectedUserId, booking.UserId);
            Assert.NotNull(booking.Invoice);
        }
    }
}