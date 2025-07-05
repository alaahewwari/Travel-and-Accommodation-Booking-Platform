using Riok.Mapperly.Abstractions;
using TABP.Application.Cities.Commands.Create;
using TABP.Application.Cities.Commands.Update;
using TABP.Application.Cities.Common;
using TABP.Domain.Entites;
namespace TABP.Application.Cities.Mapping
{
    [Mapper]
    public partial class CityMapper
    {
        protected partial City ToCityDomainInternal(CreateCityCommand command);
        protected partial City ToCityDomainInternal(UpdateCityCommand command);
        public partial CityForManagementResponse ToCityForManagementResponse(CityWithHotelCount city);
        public partial CityResponse ToCityResponse(City city);
        public City ToCityDomain(CreateCityCommand command)
        {
            var city = ToCityDomainInternal(command);
            city.CreatedAt = DateTime.UtcNow;
            city.UpdatedAt = DateTime.UtcNow;
            return city;
        }
        public City ToCityDomain(UpdateCityCommand command)
        {
            var city = ToCityDomainInternal(command);
            city.UpdatedAt = DateTime.UtcNow;
            return city;
        }
        public partial CityResponse ToCityResponse(City city);
    }
}