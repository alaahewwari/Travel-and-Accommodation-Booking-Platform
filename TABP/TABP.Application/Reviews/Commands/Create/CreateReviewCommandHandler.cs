using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Mappers;
using TABP.Application.Users.Common;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Reviews.Commands.Create
{
    public class CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IHotelRepository hotelRepository,
        IUserContext userContext,
        IUserRepository userRepository) : IRequestHandler<CreateReviewCommand, Result<ReviewResponse>>
    {
        public async Task<Result<ReviewResponse>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
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
            review.UserId = existingUser.Id;
            review.HotelId = existingHotel.Id;
            var createdReview = await reviewRepository.CreateReviewAsync(review, cancellationToken);
            return Result<ReviewResponse>.Success(createdReview.ToReviewResponse());
        }
    }
}