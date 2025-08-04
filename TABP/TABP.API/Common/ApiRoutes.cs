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
            /// <example>POST api/auth/login</example>
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
        }

        /// <summary>
        /// City management endpoints for travel destinations.
        /// </summary>
        public static class Cities
        {
            /// <summary>
            /// Retrieve a specific city by its unique identifier.
            /// </summary>
            /// <example>GET api/cities/{id}</example>
            public const string GetById = Base + "/{id}";

            /// <summary>
            /// Retrieve all cities with optional filtering and pagination.
            /// </summary>
            /// <example>GET api/cities</example>
            public const string GetAll = Base;

            /// <summary>
            /// Create a new city record.
            /// </summary>
            /// <example>POST api/cities</example>
            public const string Create = Base;

            /// <summary>
            /// Update an existing city record.
            /// </summary>
            /// <example>PUT api/cities/{id}</example>
            public const string Update = Base + "/{id}";

            /// <summary>
            /// Delete a city record.
            /// </summary>
            /// <example>DELETE api/cities/{id}</example>
            public const string Delete = Base + "/{id}";

            /// <summary>
            /// Retrieve trending/popular cities based on booking activity.
            /// </summary>
            /// <example>GET api/cities/trending</example>
            public const string GetTrending = Base + "/trending";

            /// <summary>
            /// Set or update the thumbnail image for a city.
            /// </summary>
            /// <example>POST api/cities/{id}/thumbnail</example>
            public const string SetThumbnail = Base + "/{id}/thumbnail";
        }
        /// <summary>
        /// Hotel review management endpoints for guest feedback and ratings.
        /// </summary>
        public static class Reviews
        {
            /// <summary>
            /// Retrieve specific review by ID.
            /// </summary>
            /// <example>GET api/reviews/{id}</example>
            public const string GetById = Base + "/{id}";

            /// <summary>
            /// Retrieve all reviews with filtering and pagination.
            /// </summary>
            /// <example>GET api/reviews</example>
            public const string GetAll = Base;

            /// <summary>
            /// Create new review resource.
            /// </summary>
            /// <example>POST api/reviews</example>
            public const string Create = Base;

            /// <summary>
            /// Update entire review resource.
            /// </summary>
            /// <example>PUT api/reviews/{id}</example>
            public const string Update = Base + "/{id}";

            /// <summary>
            /// Delete review resource.
            /// </summary>
            /// <example>DELETE api/reviews/{id}</example>
            public const string Delete = Base + "/{id}";
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
            /// Delete owner resource.
            /// </summary>
            /// <example>DELETE api/owners/{id}</example>
            public const string Delete = Base + "/{id}";
        }

        /// <summary>
        /// Hotel resource endpoints for accommodation properties.
        /// </summary>
        public static class Hotels
        {
            /// <summary>
            /// Search hotels with complex criteria and availability checking.
            /// </summary>
            /// <example>GET api/hotels/search</example>
            public const string Search = Base + "/search";

            /// <summary>
            /// Retrieve featured hotel deals for homepage display.
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
            /// <example>GET api/hotels</example>
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
            /// Set or update the thumbnail image for a hotel.
            /// </summary>
            /// <example>POST api/hotels/{id}/thumbnail</example>
            public const string SetThumbnail = Base + "/{id}/thumbnail";

            /// <summary>
            /// Add an image to the hotel's gallery.
            /// </summary>
            /// <example>POST api/hotels/{id}/gallery</example>
            public const string AddToGallery = Base + "/{id}/gallery";

            /// <summary>
            /// Get reviews for a specific hotel.
            /// </summary>
            /// <example>GET api/hotels/{id}/reviews</example>
            public const string GetReviews = Base + "/{id}/reviews";
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
            /// <example>GET api/room-classes</example>
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
            /// Set or update the thumbnail image for a room class.
            /// </summary>
            /// <example>POST api/room-classes/{id}/thumbnail</example>
            public const string SetThumbnail = Base + "/{id}/thumbnail";

            /// <summary>
            /// Add an image to the room class gallery.
            /// </summary>
            /// <example>POST api/room-classes/{id}/gallery</example>
            public const string AddToGallery = Base + "/{id}/gallery";

            /// <summary>
            /// Retrieve active discounts for a room class.
            /// </summary>
            /// <example>GET api/room-classes/{id}/discounts</example>
            public const string GetDiscount = Base + "/{id}/discounts";

            /// <summary>
            /// Assign an amenity to a room class.
            /// </summary>
            /// <example>POST api/room-classes/{id}/amenities</example>
            public const string AddAmenity = Base + "/{id}/amenities";
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
            /// <example>GET api/rooms</example>
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
            /// Delete room resource.
            /// </summary>
            /// <example>DELETE api/rooms/{id}</example>
            public const string Delete = Base + "/{id}";
        }

        /// <summary>
        /// Discount resource endpoints.
        /// </summary>
        public static class Discounts
        {
            /// <summary>
            /// Create new discount resource.
            /// </summary>
            /// <example>POST api/discounts</example>
            public const string Create = Base;

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
            /// Delete role resource.
            /// </summary>
            /// <example>DELETE api/roles/{id}</example>
            public const string Delete = Base + "/{id}";
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
            /// <example>GET api/bookings</example>
            public const string GetAll = Base;

            /// <summary>
            /// Create new booking resource.
            /// </summary>
            /// <example>POST api/bookings</example>
            public const string Create = Base;

            /// <summary>
            /// Cancel booking (soft delete).
            /// </summary>
            /// <example>PATCH api/bookings/{id}</example>
            public const string Cancel = Base + "/{id}";

            /// <summary>
            /// Generate PDF invoice for a booking.
            /// </summary>
            /// <example>GET api/bookings/{id}/invoice.pdf</example>
            public const string GetInvoicePdf = Base + "/{id}/invoice.pdf";
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
            /// <example>GET api/amenities</example>
            public const string GetAll = Base;

            /// <summary>
            /// Create new amenity resource.
            /// </summary>
            /// <example>POST api/amenities</example>
            public const string Create = Base;

            /// <summary>
            /// Update an existing amenity resource.
            /// </summary>
            /// <example>PUT api/amenities/{id}</example>
            public const string Update = Base + "/{id}";
        }
    }
}