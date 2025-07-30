using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Amenities;
using TABP.Application.Amenities.Commands.Create;
using TABP.Application.Amenities.Commands.Update;
using TABP.Application.Amenities.Common;
using TABP.Domain.Entities;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class AmenitiesMapping
    {
        public static partial CreateAmenityCommand ToCommand(this CreateAmenityRequest request);
        public static partial UpdateAmenityCommand ToCommand(this UpdateAmenityRequest request, long id);
        public static partial AmenityResponse ToResponse(this Amenity amenity);
    }
}