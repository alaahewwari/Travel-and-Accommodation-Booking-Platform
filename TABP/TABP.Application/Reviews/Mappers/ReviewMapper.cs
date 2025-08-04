using Riok.Mapperly.Abstractions;
using TABP.Application.Reviews.Commands.Create;
using TABP.Application.Reviews.Commands.Update;
using TABP.Application.Reviews.Common;
using TABP.Domain.Entities;
namespace TABP.Application.Reviews.Mappers
{
    [Mapper]
    public static partial class ReviewMapper
    {
        public static partial ReviewResponse ToReviewResponse(this Review review);
        private static partial Review ToDomainInternal(CreateReviewCommand command);
        public static Review ToDomain(this CreateReviewCommand command)
        {
            var review = ToDomainInternal(command);
            review.CreatedAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;
            return review;
        }
        public static Review ToDomain(this UpdateReviewCommand command)
        {
            var review = ToDomainInternal(command);
            review.UpdatedAt = DateTime.UtcNow;
            return review;
        }
        private static partial Review ToDomainInternal(UpdateReviewCommand command);
    }
}