using TABP.Domain.Entities;
using TABP.Domain.Services.Booking;
namespace TABP.Domain.Services.Pricing
{
    public sealed class PricingService : IPricingService
    {
        public decimal CalculateTotalPrice(IEnumerable<Room> rooms, int nights)
        {
            if (rooms is null) throw new ArgumentNullException(nameof(rooms));
            if (nights <= 0) throw new ArgumentException("Nights must be positive", nameof(nights));
            var basePrice = rooms.Sum(room => room.RoomClass.PricePerNight * nights);
            return basePrice;
        }
    }
}