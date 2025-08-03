using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    public class DiscountRepository(ApplicationDbContext context) : IDiscountRespository
    {
        public async Task<Discount> CreateAndAssignDiscountAsync(Discount discount,RoomClass roomClass, CancellationToken cancellationToken)
        {
            roomClass.Discount = discount;
            discount.RoomClasses.Add(roomClass);
            context.RoomClasses.Update(roomClass);
            await context.SaveChangesAsync(cancellationToken);
            return discount;
        }
        public async Task<bool> DeleteDiscountAsync(int id, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return false;
            }
            context.Discounts.Remove(discount);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<Discount?> GetDiscountByIdAsync(int id, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
            return discount;
        }
        public async Task<Discount?> GetDiscountByRoomClassAsync(long roomClassId, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts
                .Where(d => d.RoomClasses.Any(rc => rc.Id == roomClassId) && d.EndDate > DateTime.UtcNow)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return discount;
        }
    }
}