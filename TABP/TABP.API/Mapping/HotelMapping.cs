using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Hotels;
using TABP.Application.Hotels.Commands.Create;
using TABP.Application.Hotels.Commands.Update;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class HotelMapping
    {
        public static partial CreateHotelCommand ToCommand(this CreateHotelRequest request);
        public static partial UpdateHotelCommand ToCommand(this UpdateHotelRequest request, long id);
    }
}