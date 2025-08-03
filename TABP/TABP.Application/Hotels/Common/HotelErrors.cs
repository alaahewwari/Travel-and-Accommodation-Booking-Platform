using TABP.Application.Common;
namespace TABP.Application.Hotels.Common
{
    public static class HotelErrors
    {
        public static readonly Error HotelNotFound = new(
            Code: "Hotel.NotFound",
            Description: "Hotel with this ID does not exist."
        );
        public static readonly Error HotelAlreadyExists = new(
            Code: "Hotel.AlreadyExists",
            Description: "Hotel with this name already exists."
        );
        public static readonly Error InvalidHotelData = new(
            Code: "Hotel.InvalidData",
            Description: "The provided Hotel data is invalid."
        );
        public static readonly Error NotModified = new(
            Code: "Hotel.NotModified",
            Description: "The Hotel was not modified."
        );
    }
}