using Riok.Mapperly.Abstractions;
using TABP.Application.Hotels.Commands.Create;
using TABP.Application.Hotels.Commands.Update;
using TABP.Application.Hotels.Common;
using TABP.Domain.Entites;
using TABP.Domain.Models;
namespace TABP.Application.Hotels.Mapper
{
    [Mapper]
    public static partial class HotelMapper
    {
        public static partial HotelResponse ToHotelResponse(this Hotel hotel);
        public static partial HotelForManagementResponse ToHotelForManagementResponse(this HotelForManagement hotel);
        public static Hotel ToHotelDomain(this CreateHotelCommand command)
        {
            var hotel = ToHotelDomainInternal(command);
            hotel.CreatedAt = DateTime.UtcNow;
            hotel.UpdatedAt = DateTime.UtcNow;
            return hotel;
        }
        public static Hotel ToHotelDomain(this UpdateHotelCommand command)
        {
            var Hotel = ToHotelDomainInternal(command);
            Hotel.UpdatedAt = DateTime.UtcNow;
            return Hotel;
        }
        private static partial Hotel ToHotelDomainInternal(CreateHotelCommand command);
        private static partial Hotel ToHotelDomainInternal(UpdateHotelCommand command);
    }
}