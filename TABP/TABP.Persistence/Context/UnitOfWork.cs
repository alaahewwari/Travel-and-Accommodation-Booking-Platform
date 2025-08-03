using TABP.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
namespace TABP.Persistence.Context
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        public async Task ExecuteResilientTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken)
        {
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await operation(cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }
    }
}