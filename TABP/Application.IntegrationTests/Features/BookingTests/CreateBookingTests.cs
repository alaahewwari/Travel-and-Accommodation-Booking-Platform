using Application.IntegrationTests.Common;
using Application.IntegrationTests.Common.TestInfrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TABP.Application.Bookings.Commands.Create;
using TABP.Application.Bookings.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Rooms.Common;
using TABP.Application.Users.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Payment;
using TABP.Persistence.Context;
namespace Application.IntegrationTests.Features.BookingTests
{
    public class CreateBookingTests : BaseIntegrationTest
    {
        private readonly ApplicationDbContext _context;
        private readonly TestUserContext _testUserContext;
        private readonly TestDataSeeder _seeder;
        private readonly TestEmailService _emailService;
        private readonly TestPdfService _pdfService;
        private readonly TestPaymentService _paymentService;
        public CreateBookingTests(IntegrationTestWebAppFactory factory)
          : base(factory)
        {
            _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _testUserContext = (TestUserContext)_scope.ServiceProvider.GetRequiredService<IUserContext>();
            _seeder = new TestDataSeeder(_context);
            _emailService = (TestEmailService)_scope.ServiceProvider.GetRequiredService<IEmailService>();
            _pdfService = (TestPdfService)_scope.ServiceProvider.GetRequiredService<IPdfService>();
            _paymentService = (TestPaymentService)_scope.ServiceProvider.GetRequiredService<IPaymentService>();
        }
        [Fact]
        public async Task CreateBooking_ShouldSucceed_WithSingleRoomAndSuccessfulPayment()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.Success("pi_test_success"));

            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().BeGreaterThan(0);
            result.Value.HotelName.Should().Be(hotel.Name);
            result.Value.RoomNumbers.Should().HaveCount(1);
            result.Value.TotalPrice.Should().BeGreaterThan(0);
            result.Value.PaymentInfo.Should().BeNull(); // No payment info needed for successful payments

            await VerifyBookingAsync(result.Value.Id, user.Id, 1, BookingStatus.Confirmed);
            await VerifyPaymentAsync(result.Value.Id, PaymentStatus.Succeeded);
        }

        [Fact]
        public async Task CreateBooking_ShouldSucceed_WithMultipleRoomsAndSuccessfulPayment()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(3);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.Success("pi_test_success"));

            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.RoomNumbers.Should().HaveCount(3);

            await VerifyBookingAsync(result.Value.Id, user.Id, 3, BookingStatus.Confirmed);
            await VerifyPaymentAsync(result.Value.Id, PaymentStatus.Succeeded);
        }
        [Fact]
        public async Task CreateBooking_ShouldCreateInvoiceAndPayment()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.Success("pi_test_success"));

            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsSuccess.Should().BeTrue();

            var booking = await GetBookingAsync(result.Value.Id);
            booking.Invoice.Should().NotBeNull();
            booking.Invoice!.InvoiceNumber.Should().StartWith("INV-");
            booking.Invoice.Status.Should().Be(PaymentStatus.Succeeded);
            booking.Invoice.TotalAmount.Should().Be(booking.TotalPrice);
            booking.Invoice.IssueDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            // Verify payment record
            var payment = await GetPaymentByBookingIdAsync(booking.Id);
            payment.Should().NotBeNull();
            payment!.PaymentIntentId.Should().Be("pi_test_success");
            payment.Status.Should().Be(PaymentStatus.Succeeded);
            payment.Amount.Should().Be(booking.TotalPrice);
            payment.Provider.Should().Be(PaymentProvider.Stripe);
            payment.ProcessedAt.Should().NotBeNull();
            payment.ProcessedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }

        [Fact]
        public async Task CreateBooking_ShouldHandlePaymentRequiringAction()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.RequiresConfirmation("pi_test_3d_secure", "pi_test_3d_secure_secret_abc"));

            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_threeDSecure2Required");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.PaymentInfo.Should().NotBeNull();
            result.Value.PaymentInfo!.PaymentIntentId.Should().Be("pi_test_3d_secure");
            result.Value.PaymentInfo.ClientSecret.Should().Be("pi_test_3d_secure_secret_abc");
            result.Value.PaymentInfo.Status.Should().Be(PaymentStatus.RequiresAction.ToString());

            await VerifyBookingAsync(result.Value.Id, user.Id, 1, BookingStatus.Pending);
            await VerifyPaymentAsync(result.Value.Id, PaymentStatus.RequiresAction);

            // Verify no confirmation email sent yet (waiting for payment confirmation)
            _emailService.SentEmails.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateBooking_ShouldFail_WhenPaymentDeclined()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.Failed("Your card was declined."));
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_chargeDeclined");
            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            // Verify no emails sent
            _emailService.SentEmails.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateBooking_ShouldFail_WhenCheckOutBeforeCheckIn()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);

            var command = new CreateBookingCommand(
                RoomIds: rooms.Select(r => r.Id).ToList(),
                HotelId: hotel.Id,
                CheckInDate: DateTime.Today.AddDays(5),
                CheckOutDate: DateTime.Today.AddDays(2), // Invalid: before check-in
                GuestRemarks: "Test",
                PaymentMethodId: "pm_card_visa"
            );

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.InvalidBookingDates.Code);
        }

        [Fact]
        public async Task CreateBooking_ShouldFail_WhenCheckInInPast()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);

            var command = new CreateBookingCommand(
                RoomIds: rooms.Select(r => r.Id).ToList(),
                HotelId: hotel.Id,
                CheckInDate: DateTime.Today.AddDays(-1), // Past date
                CheckOutDate: DateTime.Today.AddDays(2),
                GuestRemarks: "Test",
                PaymentMethodId: "pm_card_visa"
            );

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(BookingErrors.InvalidBookingDates.Code);
        }

        [Fact]
        public async Task CreateBooking_ShouldFail_WhenHotelNotFound()
        {
            // Arrange
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);

            var command = CreateBookingCommand(999999, new List<long> { 1 }, "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(HotelErrors.HotelNotFound.Code);
        }

        [Fact]
        public async Task CreateBooking_ShouldFail_WhenRoomNotFound()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);

            var command = CreateBookingCommand(hotel.Id, new List<long> { 999999 }, "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(RoomErrors.RoomNotFound.Code);
        }

        [Fact]
        public async Task CreateBooking_ShouldFail_WhenUserNotFound()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            _testUserContext.SetUser(999999, "nonexistent@test.com"); // Non-existent user

            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(UserErrors.UserNotFound.Code);
        }

        [Fact]
        public async Task CreateBooking_ShouldSendConfirmationEmail_OnlyAfterSuccessfulPayment()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.Success("pi_test_success"));

            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_visa");

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            await VerifyBookingAsync(result.Value.Id, user.Id, 1, BookingStatus.Confirmed);

            var sentEmails = _emailService.SentEmails;
            sentEmails.Should().HaveCount(1);
            sentEmails[0].Subject.Should().Be("Booking Confirmation");
            sentEmails[0].To.Should().Contain(user.Email);

            var generatedPdfs = _pdfService.GeneratedPdfs;
            generatedPdfs.Should().HaveCount(1);
            generatedPdfs[0].Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateBooking_ShouldHandlePaymentServiceFailure()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetShouldThrow(true); // Simulate payment service exception
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), "pm_card_visa");
            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Theory]
        [InlineData("pm_card_visa", PaymentStatus.Succeeded, BookingStatus.Confirmed)]
        [InlineData("pm_card_threeDSecure2Required", PaymentStatus.RequiresAction, BookingStatus.Pending)]
        [InlineData("pm_card_chargeDeclined", PaymentStatus.Failed, null)]
        public async Task CreateBooking_ShouldHandleDifferentPaymentMethods(
            string paymentMethodId,
            PaymentStatus expectedPaymentStatus,
            BookingStatus? expectedBookingStatus)
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(1);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);

            // Configure payment service based on payment method
            var paymentResult = paymentMethodId switch
            {
                "pm_card_visa" => PaymentResult.Success("pi_success"),
                "pm_card_threeDSecure2Required" => PaymentResult.RequiresConfirmation("pi_3ds", "secret_123"),
                "pm_card_chargeDeclined" => PaymentResult.Failed("Card declined"),
                _ => PaymentResult.Failed("Unknown payment method")
            };
            _paymentService.SetPaymentResult(paymentResult);
            var command = CreateBookingCommand(hotel.Id, rooms.Select(r => r.Id).ToList(), paymentMethodId);

            // Act
            var result = await Sender.Send(command);

            // Assert
            if (expectedBookingStatus.HasValue)
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().NotBeNull();

                await VerifyBookingAsync(result.Value.Id, user.Id, 1, expectedBookingStatus.Value);
                await VerifyPaymentAsync(result.Value.Id, expectedPaymentStatus);

                if (expectedPaymentStatus == PaymentStatus.RequiresAction)
                {
                    result.Value.PaymentInfo.Should().NotBeNull();
                    result.Value.PaymentInfo!.PaymentIntentId.Should().Be("pi_3ds");
                    result.Value.PaymentInfo.ClientSecret.Should().Be("secret_123");
                }
                else if (expectedPaymentStatus == PaymentStatus.Succeeded)
                {
                    result.Value.PaymentInfo.Should().BeNull();
                }
            }
            else
            {
                result.IsFailure.Should().BeTrue();
            }
        }
        [Fact]
        public async Task CreateBooking_ShouldCalculateCorrectTotalPrice()
        {
            // Arrange
            var (hotel, roomClass, rooms) = await _seeder.SeedCompleteHotelAsync(2);
            var user = await _seeder.SeedUserAsync();
            _testUserContext.SetUser(user.Id, user.Email);
            _paymentService.SetPaymentResult(PaymentResult.Success("pi_test_success"));
            var checkInDate = DateTime.Today.AddDays(1);
            var checkOutDate = DateTime.Today.AddDays(4); // 3 nights
            var command = new CreateBookingCommand(
                RoomIds: rooms.Select(r => r.Id).ToList(),
                HotelId: hotel.Id,
                CheckInDate: checkInDate,
                CheckOutDate: checkOutDate,
                GuestRemarks: "Test",
                PaymentMethodId: "pm_card_visa"
            );

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.TotalPrice.Should().BeGreaterThan(0);
            result.Value.CheckInDate.Should().Be(checkInDate);
            result.Value.CheckOutDate.Should().Be(checkOutDate);
            var booking = await GetBookingAsync(result.Value.Id);
            booking.TotalPrice.Should().Be(result.Value.TotalPrice);
            // Verify nights calculation (3 nights * 2 rooms * room price)
            var expectedNights = (checkOutDate - checkInDate).Days;
            expectedNights.Should().Be(3);
        }

        private CreateBookingCommand CreateBookingCommand(long hotelId, List<long> roomIds, string paymentMethodId = "pm_card_visa")
        {
            return new CreateBookingCommand(
                RoomIds: roomIds,
                HotelId: hotelId,
                CheckInDate: DateTime.Today.AddDays(1),
                CheckOutDate: DateTime.Today.AddDays(4),
                GuestRemarks: "Integration test booking",
                PaymentMethodId: paymentMethodId
            );
        }

        private async Task<Booking> GetBookingAsync(long bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Invoice)
                .Include(b => b.Rooms)
                .FirstAsync(b => b.Id == bookingId);
        }

        private async Task<Payment?> GetPaymentByBookingIdAsync(long bookingId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        private async Task VerifyBookingAsync(long bookingId, long expectedUserId, int expectedRoomCount, BookingStatus expectedStatus)
        {
            var booking = await GetBookingAsync(bookingId);

            booking.Should().NotBeNull();
            booking.Status.Should().Be(expectedStatus);
            booking.Rooms.Should().HaveCount(expectedRoomCount);
            booking.UserId.Should().Be(expectedUserId);
            booking.Invoice.Should().NotBeNull();
            booking.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
            booking.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }

        private async Task VerifyPaymentAsync(long bookingId, PaymentStatus expectedStatus)
        {
            var payment = await GetPaymentByBookingIdAsync(bookingId);

            payment.Should().NotBeNull();
            payment!.Status.Should().Be(expectedStatus);
            payment.BookingId.Should().Be(bookingId);
            payment.Currency.Should().Be(PriceCurrency.USD);
            payment.Provider.Should().Be(PaymentProvider.Stripe);

            if (expectedStatus == PaymentStatus.Succeeded)
            {
                payment.ProcessedAt.Should().NotBeNull();
                payment.ProcessedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
                payment.FailureReason.Should().BeNullOrEmpty();
            }
            else if (expectedStatus == PaymentStatus.RequiresAction)
            {
                payment.ProcessedAt.Should().BeNull();
                payment.ClientSecret.Should().NotBeNullOrEmpty();
            }
        }
    }
}