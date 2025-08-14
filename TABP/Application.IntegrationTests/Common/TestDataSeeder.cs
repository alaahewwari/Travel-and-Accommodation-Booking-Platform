using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Persistence.Context;
namespace Application.IntegrationTests.Common
{
    public class TestDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private int _counter = 0;
        public TestDataSeeder(ApplicationDbContext context) => _context = context;
        public async Task<Role> SeedRoleAsync(string? roleName = null)
        {
            var role = new Role
            {
                Name = roleName ?? "Customer",
                IsDeleted = false,
                DeletedOn = default
            };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<User> SeedUserAsync(int? roleId = null)
        {
            var role = roleId ?? (await SeedRoleAsync()).Id;

            var user = new User
            {
                FirstName = "Test",
                LastName = $"User{++_counter}",
                Username = $"testuser_{Guid.NewGuid():N}"[..20],
                Email = $"user_{Guid.NewGuid():N}@test.com",
                PasswordHash = "hashed_password_value",
                Salt = "salt_value",
                RoleId = role,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                DeletedOn = default
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<Owner> SeedOwnerAsync()
        {
            var owner = new Owner
            {
                FirstName = "Hotel",
                LastName = $"Owner{++_counter}",
                PhoneNumber = "+1234567890",
                IsDeleted = false,
                DeletedOn = default
            };
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
            return owner;
        }
        public async Task<City> SeedCityAsync()
        {
            var city = new City
            {
                Name = $"TestCity{++_counter}",
                Country = "Test Country",
                PostOffice = "12345",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                DeletedOn = default
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }
        public async Task<Hotel> SeedHotelAsync(long? ownerId = null, int? cityId = null)
        {
            var owner = ownerId ?? (await SeedOwnerAsync()).Id;
            var city = cityId ?? (await SeedCityAsync()).Id;

            var hotel = new Hotel
            {
                Name = $"Test Hotel {++_counter}",
                Description = "A comfortable test hotel",
                BriefDescription = "Test hotel for integration tests",
                Address = "123 Test Street, Test City",
                StarRating = 4,
                LocationLatitude = 40.7128,
                LocationLongitude = -74.0060,
                CityId = city,
                OwnerId = owner,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsDeleted = false,
                DeletedOn = default
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task<RoomClass> SeedRoomClassAsync(
            long hotelId,
            RoomType type = RoomType.Standard,
            decimal? pricePerNight = null)
        {
            var roomClass = new RoomClass
            {
                HotelId = hotelId,
                Type = type,
                Description = "Comfortable room with modern amenities",
                BriefDescription = $"{type} room",
                PricePerNight = pricePerNight ?? 150.00m,
                AdultsCapacity = 2,
                ChildrenCapacity = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                DiscountId = null,
                IsDeleted = false,
                DeletedOn = default
            };

            _context.RoomClasses.Add(roomClass);
            await _context.SaveChangesAsync();
            return roomClass;
        }
        public async Task<Room> SeedRoomAsync(long hotelId, long roomClassId, string? roomNumber = null)
        {
            var roomCount = await _context.Rooms.CountAsync(r => r.HotelId == hotelId);

            var room = new Room
            {
                HotelId = hotelId,
                RoomClassId = roomClassId,
                Number = roomNumber ?? $"{101 + roomCount}",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsDeleted = false,
                DeletedOn = default
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task<List<Room>> SeedRoomsAsync(long hotelId, long roomClassId, int count)
        {
            var rooms = new List<Room>();
            for (int i = 0; i < count; i++)
            {
                rooms.Add(await SeedRoomAsync(hotelId, roomClassId));
            }
            return rooms;
        }
        public async Task<Booking> SeedBookingAsync(
            long userId,
            long hotelId,
            List<long> roomIds,
            DateTime? checkIn = null,
            DateTime? checkOut = null,
            PaymentMethod paymentMethod = PaymentMethod.CreditCard)
        {
            var checkInDate = checkIn ?? DateTime.Today.AddDays(1);
            var checkOutDate = checkOut ?? DateTime.Today.AddDays(4);

            var booking = new Booking
            {
                UserId = userId,
                HotelId = hotelId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                GuestRemarks = "Test booking",
                Status = BookingStatus.Pending,
                TotalPrice = 0m, // Will be calculated
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                DeletedOn = default
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            foreach (var roomId in roomIds)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO BookingRooms (BookingId, RoomId) VALUES ({0}, {1})",
                    booking.Id, roomId);
            }
            var totalPrice = await _context.Rooms
                .Where(r => roomIds.Contains(r.Id))
                .Include(r => r.RoomClass)
                .SumAsync(r => r.RoomClass.PricePerNight * (checkOutDate - checkInDate).Days);

            booking.TotalPrice = totalPrice;
            booking.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return booking;
        }
        public async Task<Invoice> SeedInvoiceAsync(long bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
                throw new InvalidOperationException($"Booking with ID {bookingId} not found");

            var invoice = new Invoice
            {
                BookingId = bookingId,
                InvoiceNumber = $"INV-{DateTime.Now:yyyyMMdd}-{++_counter:D4}",
                IssueDate = DateTime.UtcNow,
                TotalAmount = booking.TotalPrice,
                Status = PaymentStatus.Pending
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }
        public async Task<Review> SeedReviewAsync(long userId, long hotelId, int rating = 4)
        {
            var review = new Review
            {
                UserId = userId,
                HotelId = hotelId,
                Rating = rating,
                Comment = "Great hotel, would recommend!",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<Amenity> SeedAmenityAsync(string? name = null)
        {
            var amenity = new Amenity
            {
                Name = name ?? $"Amenity {++_counter}",
                Description = "Test amenity description",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsDeleted = false,
                DeletedOn = default
            };

            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }
        public async Task<(Hotel hotel, RoomClass roomClass, List<Room> rooms)>
            SeedCompleteHotelAsync(int roomCount)
        {
            var role = await SeedRoleAsync();
            var owner = await SeedOwnerAsync();
            var city = await SeedCityAsync();
            var hotel = await SeedHotelAsync(owner.Id, city.Id);
            var roomClass = await SeedRoomClassAsync(hotel.Id);
            var rooms = await SeedRoomsAsync(hotel.Id, roomClass.Id, roomCount);
            return (hotel, roomClass, rooms);
        }
    }
}