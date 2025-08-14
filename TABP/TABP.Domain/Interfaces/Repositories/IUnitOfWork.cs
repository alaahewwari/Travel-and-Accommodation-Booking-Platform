namespace TABP.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task ExecuteResilientTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken);
    }
}