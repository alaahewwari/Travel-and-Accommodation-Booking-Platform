using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.City;
using TABP.Application.Cities.Commands.Create;
using TABP.Application.Cities.Commands.Update;
namespace TABP.API.Mapping
{
    [Mapper]
    public partial class CityMapper
    {
        public partial CreateCityCommand ToCommand(CreateCityRequest request);
        protected partial UpdateCityCommand ToCommandInternal(UpdateCityRequest request, int id);
        public UpdateCityCommand ToCommand(UpdateCityRequest request, int id)
        {
            var command = ToCommandInternal(request, id);
            return new UpdateCityCommand(id, command.Name, command.Country, command.PostOffice);
        }
    }
}