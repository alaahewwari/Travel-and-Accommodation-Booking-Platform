using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the discount repository for discount data access operations.
    /// Provides concrete implementation of discount CRUD operations and room class assignment management using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for discount operations.</param>
    public class DiscountRepository(ApplicationDbContext context) : IDiscountRepository
    {
        /// <inheritdoc />
        public async Task<Discount> CreateAndAssignDiscountAsync(Discount discount, RoomClass roomClass, CancellationToken cancellationToken)
        {
            roomClass.Discount = discount;
            discount.RoomClasses.Add(roomClass);
            context.RoomClasses.Update(roomClass);
            await context.SaveChangesAsync(cancellationToken);
            return discount;
        }
        /// <inheritdoc />
        public async Task<bool> DeleteDiscountAsync(int id, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts.FindAsync([id], cancellationToken);
            if (discount == null)
            {
                return false;
            }
            context.Discounts.Remove(discount);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        /// <inheritdoc />
        public async Task<Discount?> GetDiscountByIdAsync(int id, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            return discount;
        }
    }
}