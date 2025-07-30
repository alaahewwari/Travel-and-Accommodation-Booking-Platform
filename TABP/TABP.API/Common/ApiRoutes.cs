namespace TABP.API.Common
{
    public static class ApiRoutes
    {
        public const string Base = "api/[controller]";
        public static class Identity
        {
            public const string Login = Base + "/login";
            public const string Register = Base + "/register";
        }
        public static class Cities
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            public const string GetTrending = Base + "/trending";
            public const string SetThumbnail = Base + "/{id}/thumbnail";
        }
        public static class Owners
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
        }
        public static class Hotels
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            public const string GetFeaturedDeals = Base + "/featured-deals";
            public const string Search = Base + "/search";
            public const string SetThumbnail = Base + "/{id}/thumbnail";
            public const string AddToGallery = Base + "/{id}/gallery";
        }
        public static class Rooms
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = "api/hotels/{hotelId}/room-classes/{roomClassId}/rooms";
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
        }
        public static class RoomClasses
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            public const string SetThumbnail = Base + "/{id}/thumbnail";
            public const string AddToGallery = Base + "/{id}/gallery";
        }
        public static class Discounts
        {
            public const string GetByRoomClass = "api/room-classes/{roomClassId}/discounts";
            public const string GetAll = Base;
            public const string Create = "api/room-classes/{roomClassId}/discounts";
            public const string Delete = Base + "/{id}";
        }
        public static class Roles
        {
            public const string GetAll = Base;
            public const string GetById = Base + "/{id}";
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
        }
        public static class Bookings
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Delete = Base + "/{id}";
            public const string GetInvoiceAsPdf = Base + "/{id}/invoice";
        }
        public static class Amenities
        {
            public const string GetById = Base + "/{id}";
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string AssignToRoomClass = Base + "/{id}/assign";
        }
    }
}