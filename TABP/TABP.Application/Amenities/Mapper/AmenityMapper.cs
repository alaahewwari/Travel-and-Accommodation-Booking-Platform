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
        public static partial AmenityResponse ToAmenityResponse(this Amenity amenity);
        public static Amenity ToAmenityDomain(this CreateAmenityCommand command)
        {
            var amenity = ToAmenityDomainInternal(command);
            amenity.CreatedAt = DateTime.UtcNow;
            amenity.UpdatedAt = DateTime.UtcNow;
            return amenity;
        }
        //internal 
        private static partial Amenity ToAmenityDomainInternal(CreateAmenityCommand command);
        public static Amenity ToAmenityDomain(this UpdateAmenityCommand command)
        {
            var amenity = ToAmenityDomainInternal(command);
            amenity.UpdatedAt = DateTime.UtcNow;
            return amenity;
        }
        private static partial Amenity ToAmenityDomainInternal(UpdateAmenityCommand command);
    }
}