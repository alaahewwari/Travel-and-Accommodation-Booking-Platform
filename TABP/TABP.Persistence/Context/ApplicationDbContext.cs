using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
namespace TABP.Persistence.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<CityImage> CityImages { get; set; }
        public DbSet<RoomClass> RoomClasses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Doe", Username = "johndoe", Email = "john@example.com", PasswordHash = "hashed_password_1", Salt = "salt1", CreatedAt = new DateTime(2024, 01, 01), RoleId = 1, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Email = "jane@example.com", PasswordHash = "hashed_password_2", Salt = "salt2", CreatedAt = new DateTime(2024, 01, 01), RoleId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new User { Id = 3, FirstName = "Alice", LastName = "Johnson", Username = "alicej", Email = "alice@example.com", PasswordHash = "hashed_password_3", Salt = "salt3", CreatedAt = new DateTime(2024, 01, 01), RoleId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new User { Id = 4, FirstName = "Bob", LastName = "Brown", Username = "bobb", Email = "bob@example.com", PasswordHash = "hashed_password_4", Salt = "salt4", CreatedAt = new DateTime(2024, 01, 01), RoleId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new User { Id = 5, FirstName = "Charlie", LastName = "Davis", Username = "charlied", Email = "charlie@example.com", PasswordHash = "hashed_password_5", Salt = "salt5", CreatedAt = new DateTime(2024, 01, 01), RoleId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "New York", Country = "USA", PostOffice = "10001", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new City { Id = 2, Name = "Paris", Country = "France", PostOffice = "75001", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new City { Id = 3, Name = "Tokyo", Country = "Japan", PostOffice = "10000", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new City { Id = 4, Name = "Berlin", Country = "Germany", PostOffice = "10115", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new City { Id = 5, Name = "Sydney", Country = "Australia", PostOffice = "2000", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, Percentage = 10, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 2), CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Discount { Id = 2, Percentage = 15, StartDate = new DateTime(2025, 1, 2), EndDate = new DateTime(2025, 12, 3), CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Discount { Id = 3, Percentage = 20, StartDate = new DateTime(2025, 1, 3), EndDate = new DateTime(2025, 12, 4), CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Discount { Id = 4, Percentage = 25, StartDate = new DateTime(2025, 1, 4), EndDate = new DateTime(2025, 12, 5), CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Discount { Id = 5, Percentage = 52, StartDate = new DateTime(2025, 1, 5), EndDate = new DateTime(2025, 12, 6), CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<Owner>().HasData(
                new Owner { Id = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "1234567890", IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Owner { Id = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "2345678901", IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Owner { Id = 3, FirstName = "Alice", LastName = "Johnson", PhoneNumber = "3456789012", IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Owner { Id = 4, FirstName = "Bob", LastName = "Brown", PhoneNumber = "4567890123", IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Owner { Id = 5, FirstName = "Charlie", LastName = "Davis", PhoneNumber = "5678901234", IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Role { Id = 2, Name = "Guest", IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Hotel A", Description = "Desc 0", BriefDescription = "Brief 0", Address = "Address 0", StarRating = 1, LocationLatitude = 40.0 + 0, LocationLongitude = -74.0 - 0, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), CityId = 1, OwnerId = 1, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Hotel { Id = 2, Name = "Hotel B", Description = "Desc 1", BriefDescription = "Brief 1", Address = "Address 1", StarRating = 2, LocationLatitude = 41.0, LocationLongitude = -75.0, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), CityId = 2, OwnerId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Hotel { Id = 3, Name = "Hotel C", Description = "Desc 2", BriefDescription = "Brief 2", Address = "Address 2", StarRating = 3, LocationLatitude = 42.0, LocationLongitude = -76.0, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), CityId = 3, OwnerId = 3, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Hotel { Id = 4, Name = "Hotel D", Description = "Desc 3", BriefDescription = "Brief 3", Address = "Address 3", StarRating = 4, LocationLatitude = 43.0, LocationLongitude = -77.0, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), CityId = 4, OwnerId = 4, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Hotel { Id = 5, Name = "Hotel E", Description = "Desc 4", BriefDescription = "Brief 4", Address = "Address 4", StarRating = 5, LocationLatitude = 44.0, LocationLongitude = -78.0, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), CityId = 5, OwnerId = 5, IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<RoomClass>().HasData(
                new RoomClass { Id = 1, Type = (RoomType)1, Description = "Room class 0", BriefDescription = "Brief class 0", PricePerNight = 100M, AdultsCapacity = 2, ChildrenCapacity = 2, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), HotelId = 1, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new RoomClass { Id = 2, Type = (RoomType)2, Description = "Room class 1", BriefDescription = "Brief class 1", PricePerNight = 110M, AdultsCapacity = 2, ChildrenCapacity = 2, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), HotelId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new RoomClass { Id = 3, Type = (RoomType)3, Description = "Room class 2", BriefDescription = "Brief class 2", PricePerNight = 120M, AdultsCapacity = 2, ChildrenCapacity = 2, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), HotelId = 3, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new RoomClass { Id = 4, Type = (RoomType)4, Description = "Room class 3", BriefDescription = "Brief class 3", PricePerNight = 130M, AdultsCapacity = 2, ChildrenCapacity = 2, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), HotelId = 4, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new RoomClass { Id = 5, Type = (RoomType)5, Description = "Room class 4", BriefDescription = "Brief class 4", PricePerNight = 140M, AdultsCapacity = 2, ChildrenCapacity = 2, CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), HotelId = 5, IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Number = "R001", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), RoomClassId = 1, HotelId = 1, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Room { Id = 2, Number = "R002", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), RoomClassId = 2, HotelId = 2, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Room { Id = 3, Number = "R003", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), RoomClassId = 3, HotelId = 3, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Room { Id = 4, Number = "R004", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), RoomClassId = 4, HotelId = 4, IsDeleted = false, DeletedOn = DateTime.MinValue },
                new Room { Id = 5, Number = "R005", CreatedAt = new DateTime(2024, 01, 01), UpdatedAt = new DateTime(2024, 01, 01), RoomClassId = 5, HotelId = 5, IsDeleted = false, DeletedOn = DateTime.MinValue }
            );
        }
    }
}