using TABP.Domain.Entities;
namespace TABP.Domain.Services.Booking
{
    public interface IPricingService
    {
        decimal CalculateTotalPrice(IEnumerable<Room> rooms, int nights);
    }
}