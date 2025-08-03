namespace TABP.API.Common
{
    /// <summary>
    /// RESTful API route definitions for the Travel and Booking Platform (TABP) API.
    /// Follows REST conventions with proper resource hierarchy and HTTP verbs.
    /// </summary>
    /// <remarks>
    /// All routes follow RESTful principles:
    /// - Resource-based URLs (nouns, not verbs)
    /// - Proper HTTP verb usage (GET, POST, PUT, DELETE, PATCH)
    /// - Hierarchical resource relationships
    /// - Consistent naming conventions
    /// </remarks>
    public static class ApiRoutes
    {
        /// <summary>
        /// Base route template for all API controllers.
        /// </summary>
        public const string Base = "api/[controller]";
        /// <summary>
        /// Authentication and session management endpoints.
        /// </summary>
        public static class Authentication
        {
            /// <summary>
            /// Create authentication session (login).
            /// </summary>
            /// <example>POST api/auth/sessions</example>
            public const string Login = Base + "/login";
        }
        /// <summary>
        /// User account management endpoints.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// Create new user account (registration).
            /// </summary>
            /// <example>POST api/users</example>
            public const string Register = Base;
            /// <summary>
            /// Retrieve specific user by ID.
            /// </summary>
            /// <example>GET api/users/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all users with filtering and pagination.
            /// </summary>
            /// <example>GET api/users</example>
            public const string GetAll = Base;
            /// <summary>
            /// Update user profile.
            /// </summary>
            /// <example>PUT api/users/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Partially update user profile.
            /// </summary>
            /// <example>PATCH api/users/{id}</example>
            public const string Patch = Base + "/{id}";
            /// <summary>
            /// Delete user account.
            /// </summary>
            /// <example>DELETE api/users/{id}</example>
            public const string Delete = Base + "/{id}";
        }
        /// <summary>
        /// City management endpoints for travel destinations.
        /// </summary>
        public static class Cities
        {
            /// <summary>
            /// Retrieves a specific city by its unique identifier.
            /// </summary>
            /// <example>GET api/cities/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieves all cities with optional filtering and pagination.
            /// </summary>
            /// <example>GET api/cities</example>
            public const string GetAll = Base;
            /// <summary>
            /// Creates a new city record.
            /// </summary>
            /// <example>POST api/cities</example>
            public const string Create = Base;
            /// <summary>
            /// Updates an existing city record.
            /// </summary>
            /// <example>PUT api/cities/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Deletes a city record.
            /// </summary>
            /// <example>DELETE api/cities/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieves trending/popular cities based on booking activity.
            /// </summary>
            /// <example>GET api/cities/trending</example>
            public const string GetTrending = Base + "/trending";
            /// <summary>
            /// Sets or updates the thumbnail image for a city.
            /// </summary>
            /// <example>POST api/cities/{id}/thumbnail</example>
            public const string SetThumbnail = Base + "/{id}/thumbnail";
        }
        /// <summary>
        /// Hotel owner resource endpoints.
        /// </summary>
        public static class Owners
        {
            /// <summary>
            /// Retrieve specific owner by ID.
            /// </summary>
            /// <example>GET api/owners/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all owners with filtering and pagination.
            /// </summary>
            /// <example>GET api/owners</example>
            public const string GetAll = Base;
            /// <summary>
            /// Create new owner resource.
            /// </summary>
            /// <example>POST api/owners</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire owner resource.
            /// </summary>
            /// <example>PUT api/owners/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Partially update owner resource.
            /// </summary>
            /// <example>PATCH api/owners/{id}</example>
            public const string Patch = Base + "/{id}";
            /// <summary>
            /// Delete owner resource.
            /// </summary>
            /// <example>DELETE api/owners/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieve hotels owned by specific owner.
            /// </summary>
            /// <example>GET api/owners/{id}/hotels</example>
            public const string GetHotels = Base + "/{id}/hotels";
        }
        /// <summary>
        /// Hotel resource endpoints for accommodation properties.
        /// </summary>
        public static class Hotels
        {
            /// <summary>
            /// Search hotels with complex criteria and availability checking.
            /// </summary>
            public const string Search = Base + "/search";
            /// <summary>
            /// Retrieve 3-5 featured hotel deals for homepage display.
            /// Returns optimized data with thumbnails, ratings, and discount pricing.
            /// </summary>
            /// <example>GET api/hotels/featured-deals</example>
            public const string GetFeaturedDeals = Base + "/featured-deals";
            /// <summary>
            /// Retrieve specific hotel by ID.
            /// </summary>
            /// <example>GET api/hotels/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all hotels with filtering, search, and pagination.
            /// </summary>
            public const string GetAll = Base;
            /// <summary>
            /// Create new hotel resource.
            /// </summary>
            /// <example>POST api/hotels</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire hotel resource.
            /// </summary>
            /// <example>PUT api/hotels/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Delete hotel resource.
            /// </summary>
            /// <example>DELETE api/hotels/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieve hotel's thumbnail image.
            /// </summary>
            /// <example>GET api/hotels/{id}/thumbnail</example>
            public const string GetThumbnail = Base + "/{id}/thumbnail";
            /// <summary>
            /// Update hotel's thumbnail image.
            /// </summary>
            /// <example>PUT api/hotels/{id}/thumbnail</example>
            public const string SetThumbnail = Base + "/{id}/thumbnail";
            /// <summary>
            /// Retrieve hotel's image gallery.
            /// </summary>
            /// <example>GET api/hotels/{id}/gallery</example>
            public const string GetGallery = Base + "/{id}/gallery";
            /// <summary>
            /// Add image to hotel's gallery.
            /// </summary>
            /// <example>POST api/hotels/{id}/gallery</example>
            public const string AddToGallery = Base + "/{id}/gallery";
            /// <summary>
            /// Delete specific image from hotel's gallery.
            /// </summary>
            /// <example>DELETE api/hotels/{id}/gallery/{imageId}</example>
            public const string DeleteFromGallery = Base + "/{id}/gallery/{imageId}";
            /// <summary>
            /// Retrieve room classes for specific hotel.
            /// </summary>
            /// <example>GET api/hotels/{id}/room-classes</example>
            public const string GetRoomClasses = Base + "/{id}/room-classes";
            /// <summary>
            /// Retrieve rooms for specific hotel.
            /// </summary>
            /// <example>GET api/hotels/{id}/rooms</example>
            public const string GetRooms = Base + "/{id}/rooms";
        }
        /// <summary>
        /// Room class/type resource endpoints.
        /// </summary>
        public static class RoomClasses
        {
            /// <summary>
            /// Retrieve specific room class by ID.
            /// </summary>
            /// <example>GET api/room-classes/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all room classes with filtering and pagination.
            /// </summary>
            /// <example>GET api/room-classes?hotel={hotelId}</example>
            public const string GetAll = Base;
            /// <summary>
            /// Create new room class resource.
            /// </summary>
            /// <example>POST api/room-classes</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire room class resource.
            /// </summary>
            /// <example>PUT api/room-classes/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Delete room class resource.
            /// </summary>
            /// <example>DELETE api/room-classes/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieve room class thumbnail image.
            /// </summary>
            /// <example>GET api/room-classes/{id}/thumbnail</example>
            public const string GetThumbnail = Base + "/{id}/thumbnail";
            /// <summary>
            /// Update room class thumbnail image.
            /// </summary>
            /// <example>PUT api/room-classes/{id}/thumbnail</example>
            public const string SetThumbnail = Base + "/{id}/thumbnail";
            /// <summary>
            /// Retrieve room class image gallery.
            /// </summary>
            /// <example>GET api/room-classes/{id}/gallery</example>
            public const string GetGallery = Base + "/{id}/gallery";
            /// <summary>
            /// Add image to room class gallery.
            /// </summary>
            /// <example>POST api/room-classes/{id}/gallery</example>
            public const string AddToGallery = Base + "/{id}/gallery";
            /// <summary>
            /// Retrieve rooms of specific room class.
            /// </summary>
            /// <example>GET api/room-classes/{id}/rooms</example>
            public const string GetRooms = Base + "/{id}/rooms";
            /// <summary>
            /// Retrieve discount for specific room class.
            /// </summary>
            /// <example>GET api/room-classes/{id}/discount</example>
            public const string GetDiscount = Base + "/{id}/discounts";
            /// <summary>
            /// Create discount for specific room class.
            /// </summary>
            /// <example>POST api/room-classes/{id}/discounts</example>
            public const string CreateDiscount = Base + "/{id}/discounts";
            /// <summary>
            /// Retrieve amenities for specific room class.
            /// </summary>
            /// <example>GET api/room-classes/{id}/amenities</example>
            public const string GetAmenities = Base + "/{id}/amenities";
            /// <summary>
            /// Assign amenity to room class.
            /// </summary>
            /// <example>POST api/room-classes/{id}/amenities</example>
            public const string AddAmenity = Base + "/{id}/amenities";
            /// <summary>
            /// Remove amenity from room class.
            /// </summary>
            /// <example>DELETE api/room-classes/{id}/amenities/{amenityId}</example>
            public const string RemoveAmenity = Base + "/{id}/amenities/{amenityId}";
        }
        /// <summary>
        /// Individual room resource endpoints.
        /// </summary>
        public static class Rooms
        {
            /// <summary>
            /// Retrieve specific room by ID.
            /// </summary>
            /// <example>GET api/rooms/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all rooms with filtering and pagination.
            /// </summary>
            /// <example>GET api/rooms?hotel={hotelId}&amp;roomClass={roomClassId}&amp;available=true</example>
            public const string GetAll = Base;
            /// <summary>
            /// Create new room resource.
            /// </summary>
            /// <example>POST api/rooms</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire room resource.
            /// </summary>
            /// <example>PUT api/rooms/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Partially update room resource.
            /// </summary>
            /// <example>PATCH api/rooms/{id}</example>
            public const string Patch = Base + "/{id}";
            /// <summary>
            /// Delete room resource.
            /// </summary>
            /// <example>DELETE api/rooms/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieve bookings for specific room.
            /// </summary>
            /// <example>GET api/rooms/{id}/bookings</example>
            public const string GetBookings = Base + "/{id}/bookings";
        }
        /// <summary>
        /// Discount resource endpoints.
        /// </summary>
        public static class Discounts
        {
            /// <summary>
            /// Retrieve specific discount by ID.
            /// </summary>
            /// <example>GET api/discounts/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all discounts with filtering and pagination.
            /// </summary>
            /// <example>GET api/discounts?active=true&amp;roomClass={roomClassId}</example>
            public const string GetAll = Base;
            /// <summary>
            /// Create new discount resource.
            /// </summary>
            /// <example>POST api/discounts</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire discount resource.
            /// </summary>
            /// <example>PUT api/discounts/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Delete discount resource.
            /// </summary>
            /// <example>DELETE api/discounts/{id}</example>
            public const string Delete = Base + "/{id}";
        }
        /// <summary>
        /// User role resource endpoints.
        /// </summary>
        public static class Roles
        {
            /// <summary>
            /// Retrieve all user roles.
            /// </summary>
            /// <example>GET api/roles</example>
            public const string GetAll = Base;
            /// <summary>
            /// Retrieve specific role by ID.
            /// </summary>
            /// <example>GET api/roles/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Create new role resource.
            /// </summary>
            /// <example>POST api/roles</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire role resource.
            /// </summary>
            /// <example>PUT api/roles/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Partially update role resource.
            /// </summary>
            /// <example>PATCH api/roles/{id}</example>
            public const string Patch = Base + "/{id}";
            /// <summary>
            /// Delete role resource.
            /// </summary>
            /// <example>DELETE api/roles/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieve users assigned to specific role.
            /// </summary>
            /// <example>GET api/roles/{id}/users</example>
            public const string GetUsers = Base + "/{id}/users";
        }
        /// <summary>
        /// Booking resource endpoints for reservations.
        /// </summary>
        public static class Bookings
        {
            /// <summary>
            /// Retrieve specific booking by ID.
            /// </summary>
            /// <example>GET api/bookings/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all bookings with filtering and pagination.
            /// </summary>
            /// <example>GET api/bookings?user={userId}&amp;hotel={hotelId}&amp;status=confirmed</example>
            public const string GetAll = Base;
            /// <summary>
            /// Create new booking resource.
            /// </summary>
            /// <example>POST api/bookings</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire booking resource.
            /// </summary>
            /// <example>PUT api/bookings/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Partially update booking (e.g., status change).
            /// </summary>
            /// <example>PATCH api/bookings/{id}</example>
            public const string Patch = Base + "/{id}";
            /// <summary>
            /// Cancel booking (soft delete).
            /// </summary>
            /// <example>Patch api/bookings/{id}</example>
            public const string Cancel = Base + "/{id}";
            /// <summary>
            /// Retrieve booking invoice.
            /// </summary>
            /// <example>GET api/bookings/{id}/invoice</example>
            public const string GetInvoice = Base + "/{id}/invoice";
            /// <summary>
            /// Generate PDF invoice for booking.
            /// </summary>
            /// <example>GET api/bookings/{id}/invoice.pdf</example>
            public const string GetInvoicePdf = Base + "/{id}/invoice.pdf";
            /// <summary>
            /// Confirm booking payment.
            /// </summary>
            /// <example>POST api/bookings/{id}/payment</example>
            public const string ProcessPayment = Base + "/{id}/payment";
        }
        /// <summary>
        /// Amenity resource endpoints.
        /// </summary>
        public static class Amenities
        {
            /// <summary>
            /// Retrieve specific amenity by ID.
            /// </summary>
            /// <example>GET api/amenities/{id}</example>
            public const string GetById = Base + "/{id}";
            /// <summary>
            /// Retrieve all amenities with filtering and pagination.
            /// </summary>
            /// <example>GET api/amenities?category=recreational</example>
            public const string GetAll = Base;
            /// <summary>
            /// Create new amenity resource.
            /// </summary>
            /// <example>POST api/amenities</example>
            public const string Create = Base;
            /// <summary>
            /// Update entire amenity resource.
            /// </summary>
            /// <example>PUT api/amenities/{id}</example>
            public const string Update = Base + "/{id}";
            /// <summary>
            /// Partially update amenity resource.
            /// </summary>
            /// <example>PATCH api/amenities/{id}</example>
            public const string Patch = Base + "/{id}";
            /// <summary>
            /// Delete amenity resource.
            /// </summary>
            /// <example>DELETE api/amenities/{id}</example>
            public const string Delete = Base + "/{id}";
            /// <summary>
            /// Retrieve room classes that have this amenity.
            /// </summary>
            /// <example>GET api/amenities/{id}/room-classes</example>
            public const string GetRoomClasses = Base + "/{id}/room-classes";
        }
    }
}