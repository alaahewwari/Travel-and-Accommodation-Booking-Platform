using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Mappers;
using TABP.Application.Users.Common;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Hotels.Queries.GetReviews
{
    public class GetReviewsByHotelQueryHandler(
        IUserContext userContext,
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IHotelRepository hotelRepository) : IRequestHandler<GetReviewsByHotelQuery, Result<IEnumerable<ReviewResponse>>>
    {
        public async Task<Result<IEnumerable<ReviewResponse>>> Handle(GetReviewsByHotelQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByIdAsync(userContext.UserId, cancellationToken);
            if (existingUser is null)
            {
                return Result<IEnumerable<ReviewResponse>>.Failure(UserErrors.UserNotFound);
            }
            var existingHotel = await hotelRepository.GetHotelByIdAsync(request.HotelId, cancellationToken);
            if (existingHotel is null)
            {
                return Result<IEnumerable<ReviewResponse>>.Failure(HotelErrors.HotelNotFound);
            }
            var reviews = await reviewRepository.GetReviewsByHotelIdAsync(request.HotelId, cancellationToken);
            var response = reviews.Select(r => r.ToReviewResponse());
            return Result<IEnumerable<ReviewResponse>>.Success(response);
        }
    }
}