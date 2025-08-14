using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapper;
using TABP.Application.Users.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Queries.GetRecentlyVisited
{
    public class GetRecentlyVisitedHotelsQueryHandler(
        IUserRepository userRepository,
        IHotelRepository hotelRepository
    ) : IRequestHandler<GetRecentlyVisitedHotelsQuery, Result<IEnumerable<HotelResponse>>>
    {
        public async Task<Result<IEnumerable<HotelResponse>>> Handle(GetRecentlyVisitedHotelsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
            if (existingUser is null)
            {
                return Result<IEnumerable<HotelResponse>>.Failure(UserErrors.UserNotFound);
            }
            var hotels = await hotelRepository.GetRecentlyVisitedHotelsAsync(request.UserId,request.Count, cancellationToken);
            var response = hotels.Select(h => h.ToHotelResponse());
            return Result<IEnumerable<HotelResponse>>.Success(response);
        }
    }
}