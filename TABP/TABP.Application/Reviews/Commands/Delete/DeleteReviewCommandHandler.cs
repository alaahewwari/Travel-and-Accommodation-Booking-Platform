using MediatR;
using TABP.Application.Common;
using TABP.Application.Reviews.Common;
using TABP.Application.Users.Common;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Reviews.Commands.Delete
{
    public class DeleteReviewCommandHandler(
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IUserContext userContext) : IRequestHandler<DeleteReviewCommand, Result>
    {
        public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
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
            await reviewRepository.DeleteReviewAsync(existingReview, cancellationToken);
            return Result.Success();
        }
    }
}