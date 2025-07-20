using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TABP.Domain.Entities;
namespace TABP.Persistence
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Owner" },
                new Role { Id = 3, Name = "Customer" },
                new Role { Id = 4, Name = "Manager" },
                new Role { Id = 5, Name = "Guest" }
            );
            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "User1", LastName = "Surname1", Username = "user1", Email = "user1@example.com", PasswordHash = "hashed_pwd", Salt = "salt", CreatedAt = new DateTime(2023, 1, 1), RoleId = 1 },
                new User { Id = 2, FirstName = "User2", LastName = "Surname2", Username = "user2", Email = "user2@example.com", PasswordHash = "hashed_pwd", Salt = "salt", CreatedAt = new DateTime(2023, 1, 1), RoleId = 2 },
                new User { Id = 3, FirstName = "User3", LastName = "Surname3", Username = "user3", Email = "user3@example.com", PasswordHash = "hashed_pwd", Salt = "salt", CreatedAt = new DateTime(2023, 1, 1), RoleId = 3 },
                new User { Id = 4, FirstName = "User4", LastName = "Surname4", Username = "user4", Email = "user4@example.com", PasswordHash = "hashed_pwd", Salt = "salt", CreatedAt = new DateTime(2023, 1, 1), RoleId = 4 },
                new User { Id = 5, FirstName = "User5", LastName = "Surname5", Username = "user5", Email = "user5@example.com", PasswordHash = "hashed_pwd", Salt = "salt", CreatedAt = new DateTime(2023, 1, 1), RoleId = 5 }
            );
            // Cities
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "City1", Country = "Country1", PostOffice = "PO1", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 2) },
                new City { Id = 2, Name = "City2", Country = "Country2", PostOffice = "PO2", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 2) },
                new City { Id = 3, Name = "City3", Country = "Country3", PostOffice = "PO3", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 2) },
                new City { Id = 4, Name = "City4", Country = "Country4", PostOffice = "PO4", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 2) },
                new City { Id = 5, Name = "City5", Country = "Country5", PostOffice = "PO5", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 2) }
            );
            // Owners
            modelBuilder.Entity<Owner>().HasData(
                new Owner { Id = 1, FirstName = "Owner1", LastName = "OwnerLast1", PhoneNumber = "0123456780" },
                new Owner { Id = 2, FirstName = "Owner2", LastName = "OwnerLast2", PhoneNumber = "0123456781" },
                new Owner { Id = 3, FirstName = "Owner3", LastName = "OwnerLast3", PhoneNumber = "0123456782" },
                new Owner { Id = 4, FirstName = "Owner4", LastName = "OwnerLast4", PhoneNumber = "0123456783" },
                new Owner { Id = 5, FirstName = "Owner5", LastName = "OwnerLast5", PhoneNumber = "0123456784" }
            );
            // Hotels
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Hotel1", Description = "Full Desc", BriefDescription = "Brief Desc", Address = "Address 1", StarRating = 4, LocationLatitude = 48.85, LocationLongitude = 2.35, CreatedAt = new DateTime(2023, 1, 1), CityId = 1, OwnerId = 1 },
                new Hotel { Id = 2, Name = "Hotel2", Description = "Full Desc", BriefDescription = "Brief Desc", Address = "Address 2", StarRating = 3, LocationLatitude = 48.86, LocationLongitude = 2.36, CreatedAt = new DateTime(2023, 1, 1), CityId = 2, OwnerId = 2 },
                new Hotel { Id = 3, Name = "Hotel3", Description = "Full Desc", BriefDescription = "Brief Desc", Address = "Address 3", StarRating = 5, LocationLatitude = 48.87, LocationLongitude = 2.37, CreatedAt = new DateTime(2023, 1, 1), CityId = 3, OwnerId = 3 },
                new Hotel { Id = 4, Name = "Hotel4", Description = "Full Desc", BriefDescription = "Brief Desc", Address = "Address 4", StarRating = 4, LocationLatitude = 48.88, LocationLongitude = 2.38, CreatedAt = new DateTime(2023, 1, 1), CityId = 4, OwnerId = 4 },
                new Hotel { Id = 5, Name = "Hotel5", Description = "Full Desc", BriefDescription = "Brief Desc", Address = "Address 5", StarRating = 3, LocationLatitude = 48.89, LocationLongitude = 2.39, CreatedAt = new DateTime(2023, 1, 1), CityId = 5, OwnerId = 5 }
            );
            // RoomClasses
            modelBuilder.Entity<RoomClass>().HasData(
                new RoomClass { Id = 1, Type = 0, BriefDescription = "Brief Room", Description = "Full Room Desc", PricePerNight = 100, AdultsCapacity = 2, ChildrenCapacity = 1, CreatedAt = new DateTime(2023, 1, 1), HotelId = 1 },
                new RoomClass { Id = 2, Type = 0, BriefDescription = "Brief Room", Description = "Full Room Desc", PricePerNight = 150, AdultsCapacity = 2, ChildrenCapacity = 1, CreatedAt = new DateTime(2023, 1, 1), HotelId = 2 },
                new RoomClass { Id = 3, Type = 0, BriefDescription = "Brief Room", Description = "Full Room Desc", PricePerNight = 200, AdultsCapacity = 2, ChildrenCapacity = 1, CreatedAt = new DateTime(2023, 1, 1), HotelId = 3 },
                new RoomClass { Id = 4, Type = 0, BriefDescription = "Brief Room", Description = "Full Room Desc", PricePerNight = 250, AdultsCapacity = 2, ChildrenCapacity = 1, CreatedAt = new DateTime(2023, 1, 1), HotelId = 4 },
                new RoomClass { Id = 5, Type = 0, BriefDescription = "Brief Room", Description = "Full Room Desc", PricePerNight = 300, AdultsCapacity = 2, ChildrenCapacity = 1, CreatedAt = new DateTime(2023, 1, 1), HotelId = 5 }
            );
            // Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Number = "Room1", CreatedAt = new DateTime(2023, 1, 1), RoomClassId = 1 },
                new Room { Id = 2, Number = "Room2", CreatedAt = new DateTime(2023, 1, 1), RoomClassId = 2 },
                new Room { Id = 3, Number = "Room3", CreatedAt = new DateTime(2023, 1, 1), RoomClassId = 3 },
                new Room { Id = 4, Number = "Room4", CreatedAt = new DateTime(2023, 1, 1), RoomClassId = 4 },
                new Room { Id = 5, Number = "Room5", CreatedAt = new DateTime(2023, 1, 1), RoomClassId = 5 }
            );
            // Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, Rating = 5, Comment = "Great", CreatedAt = new DateTime(2023, 1, 1), UserId = 1, HotelId = 1 },
                new Review { Id = 2, Rating = 4, Comment = "Good", CreatedAt = new DateTime(2023, 1, 1), UserId = 2, HotelId = 2 },
                new Review { Id = 3, Rating = 3, Comment = "Okay", CreatedAt = new DateTime(2023, 1, 1), UserId = 3, HotelId = 3 },
                new Review { Id = 4, Rating = 2, Comment = "Bad", CreatedAt = new DateTime(2023, 1, 1), UserId = 4, HotelId = 4 },
                new Review { Id = 5, Rating = 1, Comment = "Terrible", CreatedAt = new DateTime(2023, 1, 1), UserId = 5, HotelId = 5 }
            );
        }
    }
}