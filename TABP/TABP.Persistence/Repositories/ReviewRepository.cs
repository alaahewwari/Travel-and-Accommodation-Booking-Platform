using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// EF Core implementation of <see cref="IReviewRepository"/>, with best practices for data access.
    /// </summary>
    internal class ReviewRepository(ApplicationDbContext context) : IReviewRepository
    {
        /// <inheritdoc/>
        public async Task<Review> CreateReviewAsync(Review review, CancellationToken cancellationToken)
        {
            context.Reviews.Add(review);
            await context.SaveChangesAsync(cancellationToken);
            return review;
        }
        /// <inheritdoc/>
        public async Task<Review?> GetReviewByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(long hotelId, CancellationToken cancellationToken)
        {
            return await context.Reviews
                .AsNoTracking()
                .Where(r => r.HotelId == hotelId)
                .ToListAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<bool> UpdateReviewAsync(Review review, CancellationToken cancellationToken)
        {
            var existing = await GetReviewByIdAsync(review.Id, cancellationToken);
            var affected = await context.Reviews
               .Where(r => r.Id == review.Id)
               .ExecuteUpdateAsync(s => s
                   .SetProperty(r => r.Rating, r => review.Rating)
                   .SetProperty(r => r.Comment, r => review.Comment)
                   .SetProperty(r => r.UpdatedAt, r => review.UpdatedAt)
               );
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
        /// <inheritdoc/>
        public async Task DeleteReviewAsync(Review review, CancellationToken cancellationToken)
        {
            context.Reviews.Remove(review);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}