using Riok.Mapperly.Abstractions;
using TABP.Application.Cities.Commands.Create;
using TABP.Application.Cities.Commands.SetThumbnail;
using TABP.Application.Cities.Commands.Update;
using TABP.Application.Cities.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.City;
namespace TABP.Application.Cities.Mapper
{
    [Mapper]
    public static partial class CityMapper
    {
        public static partial TrendingCityResponse ToTrendingCityResponse(this City city);
        public static partial CityForManagementResponse ToCityForManagementResponse(this CityForManagement city);
        public static partial CityResponse ToCityResponse(this City city);
        public static City ToCityDomain(this CreateCityCommand command)
        {
            var city = ToCityDomainInternal(command);
            city.CreatedAt = DateTime.UtcNow;
            city.UpdatedAt = DateTime.UtcNow;
            return city;
        }
        public static City ToCityDomain(this UpdateCityCommand command)
        {
            var city = ToCityDomainInternal(command);
            city.UpdatedAt = DateTime.UtcNow;
            return city;
        }
        private static partial City ToCityDomainInternal(CreateCityCommand command);
        private static partial City ToCityDomainInternal(UpdateCityCommand command);
        [MapperIgnoreSource(nameof(SetCityThumbnailCommand.FileStream))]
        [MapperIgnoreSource(nameof(SetCityThumbnailCommand.FileName))]
        private static partial CityImage ToCityImageDomainInternal(SetCityThumbnailCommand command, string imageUrl);
        public static CityImage ToCityImageDomain(this SetCityThumbnailCommand command, string imageUrl, ImageType type)
        {
            var cityImage = ToCityImageDomainInternal(command, imageUrl);
            cityImage.CreatedAt = DateTime.UtcNow;
            cityImage.ImageType = type;
            return cityImage;
        }
    }
}