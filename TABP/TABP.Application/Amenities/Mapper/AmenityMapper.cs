using Riok.Mapperly.Abstractions;
using TABP.Application.Amenities.Commands.Create;
using TABP.Application.Amenities.Commands.Update;
using TABP.Application.Amenities.Common;
using TABP.Domain.Entities;
namespace TABP.Application.Amenities.Mapper
{
    [Mapper]
    public static partial class AmenityMapper
    {
        public static partial AmenityResponse ToAmenityResponse(this Amenity Amenity);
        public static partial Amenity ToAmenityDomain(this CreateAmenityCommand command);
        public static partial Amenity ToAmenityDomain(this UpdateAmenityCommand command);
    }
}