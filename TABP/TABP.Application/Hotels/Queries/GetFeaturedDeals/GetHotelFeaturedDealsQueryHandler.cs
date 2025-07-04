//using MediatR;
//using TABP.Application.Common;
//using TABP.Application.Hotels.Common;
//using TABP.Application.Hotels.Mapping;
//using TABP.Domain.Interfaces.Repositories;
//namespace TABP.Application.Hotels.Queries.GetFeaturedDeals
//{
//    public class GetHotelFeaturedDealsQueryHandler(
//        IRoomClassRepository roomClassRepository,
//        HotelMapper mapper
//        ) : IRequestHandler<GetHotelFeaturedDealsQuery, Result<IEnumerable<HotelFeaturedDealsResponse>>>
//    {
//        public async Task<Result<IEnumerable<HotelFeaturedDealsResponse>>> Handle(GetHotelFeaturedDealsQuery request, CancellationToken cancellationToken)
//        {
//            var hotels = await roomClassRepository.GetFeaturedDealsAsync(request.Count, cancellationToken);
//            var hotelResponses = hotels.Select(mapper.ToHotelFeaturedDealsResponse);
//            return Result<IEnumerable<HotelFeaturedDealsResponse>>.Success(hotelResponses);
//        }
//    }
//}