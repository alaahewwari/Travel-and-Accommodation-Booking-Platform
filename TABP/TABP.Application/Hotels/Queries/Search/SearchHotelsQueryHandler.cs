using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Mapper;
using TABP.Domain.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Queries.Search
{
    public class SearchHotelsQueryHandler(IHotelRepository hotelRepository) : IRequestHandler<SearchHotelsQuery, Result<HotelPaginationResult>>
    {
        public async Task<Result<HotelPaginationResult>> Handle(SearchHotelsQuery request, CancellationToken cancellationToken)
        {
            var queryParameters = request.ToSearchParameters();
            var sieveModel = request.ToSieveModel();
            var hotels = await hotelRepository.SearchAsync(queryParameters, sieveModel, cancellationToken);
            var response = hotels.ToHotelPaginationResult();
            return Result<HotelPaginationResult>.Success(response);
        }
    }
}