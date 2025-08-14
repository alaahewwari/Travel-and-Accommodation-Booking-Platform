using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Cities;
using TABP.API.Contracts.Images;
using TABP.Application.Cities.Commands.Create;
using TABP.Application.Cities.Commands.SetThumbnail;
using TABP.Application.Cities.Commands.Update;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class CityMapper
    {
        public static partial CreateCityCommand ToCommand(this CreateCityRequest request);
        public static partial UpdateCityCommand ToCommand(this UpdateCityRequest request, int id);
        public static partial SetCityThumbnailCommand ToCityCommand(this SetImageRequest request, int cityId, Stream fileStream, string fileName);
    }
}