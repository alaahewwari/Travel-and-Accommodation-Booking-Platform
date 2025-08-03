using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.RoomClasses.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Queries.GetFeaturedDeals
{
    public class GetHotelFeaturedDealsQueryHandler(
        IRoomClassRepository roomClassRepository
        ) : IRequestHandler<GetHotelFeaturedDealsQuery, Result<IEnumerable<FeaturedDealsHotelsResponse>>>
    {
        public async Task<Result<IEnumerable<FeaturedDealsHotelsResponse>>> Handle(GetHotelFeaturedDealsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await roomClassRepository.GetFeaturedDealsInHotelsAsync(request.Count, cancellationToken);
            var hotelResponses = hotels.Select(h => h.ToFeaturedDealsHotelsResponse());
            return Result<IEnumerable<FeaturedDealsHotelsResponse>>.Success(hotelResponses);
        }
    }
}