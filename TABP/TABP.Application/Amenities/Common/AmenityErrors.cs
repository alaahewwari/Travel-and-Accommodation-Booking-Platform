using TABP.Application.Common;
namespace TABP.Application.Amenities.Common
{
    public static class AmenityErrors
    {
        public static readonly Error AmenityNotFound = new(
            Code: "Amenity.NotFound",
            Description: "Amenity with this ID does not exist."
        );
        public static readonly Error AmenityAlreadyExists = new(
            Code: "Amenity.AlreadyExists",
            Description: "Amenity with this name already exists."
        );
        public static readonly Error AmenityUpdateFailed = new(
            Code: "Amenity.UpdateFailed",
            Description: "No changes were made to the amenity."
        );
    }
}