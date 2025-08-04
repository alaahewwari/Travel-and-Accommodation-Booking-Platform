using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Mappers;
using TABP.Application.Users.Common.Errors;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Reviews.Commands.Update
{
    public class UpdateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IHotelRepository hotelRepository,
        IUserContext userContext,
        IUserRepository userRepository
        ) : IRequestHandler<UpdateReviewCommand, Result>
    {
        public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByIdAsync(userContext.UserId, cancellationToken);
            if (existingUser is null)
            {
                return Result<ReviewResponse>.Failure(UserErrors.UserNotFound);
            }
            var existingHotel = await hotelRepository.GetHotelByIdAsync(request.HotelId, cancellationToken);
            if (existingHotel is null)
            {
                return Result<ReviewResponse>.Failure(HotelErrors.HotelNotFound);
            }
            var review = request.ToDomain();
            var updated = await reviewRepository.UpdateReviewAsync(review, cancellationToken);
            if (!updated)
            {
                return Result<ReviewResponse>.Failure(ReviewErrors.ReviewUpdateFailed);
            }
            return Result.Success();
        }
    }
}