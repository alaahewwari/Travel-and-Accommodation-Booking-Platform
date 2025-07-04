using Riok.Mapperly.Abstractions;
using TABP.Application.Hotels.Commands.Create;
using TABP.Application.Hotels.Commands.Update;
using TABP.Application.Hotels.Common;
using TABP.Domain.Entites;
namespace TABP.Application.Hotels.Mapping
{
    [Mapper]
    public partial class HotelMapper
    {
        protected partial Hotel ToHotelDomainInternal(CreateHotelCommand command);
        protected partial Hotel ToHotelDomainInternal(UpdateHotelCommand command);
        public partial HotelResponse ToHotelResponse(Hotel hotel);
        public Hotel ToHotelDomain(CreateHotelCommand command)
        {
            var hotel = ToHotelDomainInternal(command);
            hotel.CreatedAt = DateTime.UtcNow;
            hotel.UpdatedAt = DateTime.UtcNow;
            return hotel;
        }
        public Hotel ToHotelDomain(UpdateHotelCommand command)
        {
            var Hotel = ToHotelDomainInternal(command);
            Hotel.UpdatedAt = DateTime.UtcNow;
            return Hotel;
        }
    }
}