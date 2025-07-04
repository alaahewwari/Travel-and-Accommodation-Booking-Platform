using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Hotels;
using TABP.Application.Hotels.Commands.Create;
using TABP.Application.Hotels.Commands.Update;
namespace TABP.API.Mapping
{
    [Mapper]
    public partial class HotelMapper
    {
        public partial CreateHotelCommand ToCommand(CreateHotelRequest request);
        protected partial UpdateHotelCommand ToCommandInternal(UpdateHotelRequest request, int id);
        public UpdateHotelCommand ToCommand(UpdateHotelRequest request, int id)
        {
            var command = ToCommandInternal(request, id);
            return new UpdateHotelCommand(id,
                request.Name,
                request.CityId,
                request.OwnerId,
                request.LocationLatitude,
                request.LocationLongitude,
                request.Address
            );
        }
    }
}