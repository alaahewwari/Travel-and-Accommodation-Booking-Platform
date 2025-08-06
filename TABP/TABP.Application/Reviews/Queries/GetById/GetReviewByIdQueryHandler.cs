using MediatR;
using TABP.Application.Common;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Mappers;
using TABP.Application.Users.Common;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Reviews.Queries.GetById
{
    public class GetReviewByIdQueryHandler
        (IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IUserContext userContext) : IRequestHandler<GetReviewByIdQuery, Result>
    {
        public async Task<Result> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByIdAsync(userContext.UserId, cancellationToken);
            if (existingUser is null)
            {
                return Result.Failure(UserErrors.UserNotFound);
            }
            var existingReview = await reviewRepository.GetReviewByIdAsync(request.Id, cancellationToken);
            if (existingReview is null)
            {
                return Result.Failure(ReviewErrors.ReviewNotFound);
            }
            return Result<ReviewResponse>.Success(existingReview.ToReviewResponse());
        }
    }
}