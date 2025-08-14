using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the payment repository for payment transaction data access operations.
    /// Provides concrete implementation of payment CRUD operations and transaction tracking using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for payment operations.</param>
    public class PaymentRepository(ApplicationDbContext context) : IPaymentRepository
    {
        /// <inheritdoc />
        public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken)
        {
            var created = await context.Payments.AddAsync(payment, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return created.Entity;
        }
        /// <inheritdoc />
        public async Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Payments
                .Include(p => p.Booking)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Payment?> GetByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken)
        {
            return await context.Payments
                .Include(p => p.Booking)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PaymentIntentId == paymentIntentId, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Payment?> UpdateAsync(Payment payment, CancellationToken cancellationToken)
        {
            var updated = await context.Payments
                .Where(p => p.Id == payment.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Status, payment.Status)
                    .SetProperty(p => p.ProcessedAt, payment.ProcessedAt)
                    .SetProperty(p => p.FailureReason, payment.FailureReason));
            if (updated == 0) return null;
            return await GetByIdAsync(payment.Id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Payment>> GetByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
        {
            return await context.Payments
                .Where(p => p.BookingId == bookingId)
                .OrderBy(p => p.ProcessedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}