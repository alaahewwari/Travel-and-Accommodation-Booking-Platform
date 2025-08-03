using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IDiscountRespository
    {
        Task<Discount?> GetDiscountByRoomClassAsync(long roomClassId, CancellationToken cancellationToken);
        Task<Discount> CreateAndAssignDiscountAsync(Discount discount, RoomClass roomClass, CancellationToken cancellationToken);
        Task<bool> DeleteDiscountAsync(int id, CancellationToken cancellationToken);
        Task<Discount?> GetDiscountByIdAsync(int id, CancellationToken cancellationToken);
    }
}