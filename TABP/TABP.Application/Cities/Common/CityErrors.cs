using TABP.Application.Common;

namespace TABP.Application.Cities.Common
{
    public static class CityErrors
    {
        public static readonly Error CityNotFound = new(
            Code: "City.NotFound",
            Description: "City with this ID does not exist."
        );
        public static readonly Error CityAlreadyExists = new(
            Code: "City.AlreadyExists",
            Description: "City with this name already exists."
        );
        public static readonly Error InvalidCityData = new(
            Code: "City.InvalidData",
            Description: "The provided city data is invalid."
        );
    }
}