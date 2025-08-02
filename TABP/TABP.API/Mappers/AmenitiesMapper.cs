using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Amenities;
using TABP.Application.Amenities.Commands.Create;
using TABP.Application.Amenities.Commands.Update;
namespace TABP.API.Mappers
{
    /// <summary>
    /// Maps amenity data between requests, commands, and responses.
    /// </summary>
    [Mapper]
    public static partial class AmenitiesMapper
    {
        /// <summary>
        /// Maps a create request to a create command.
        /// </summary>
        public static partial CreateAmenityCommand ToCommand(this CreateAmenityRequest request);
        /// <summary>
        /// Maps an update request to an update command with an ID.
        /// </summary>
        public static partial UpdateAmenityCommand ToCommand(this UpdateAmenityRequest request, long id);
    }
}