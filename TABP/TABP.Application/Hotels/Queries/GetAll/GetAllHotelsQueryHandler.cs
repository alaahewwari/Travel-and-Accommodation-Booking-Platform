using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapping;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Queries.GetAll
{
    public class GetAllHotelsQueryHandler(
        IHotelRepository hotelRepository,
        HotelMapper mapper
        ) : IRequestHandler<GetAllHotelsQuery, Result<IEnumerable<HotelResponse>>>
    {
        public async Task<Result<IEnumerable<HotelResponse>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await hotelRepository.GetAllHotelsAsync(cancellationToken);
            var hotelsResponse = hotels.Select(mapper.ToHotelResponse);
            return Result<IEnumerable<HotelResponse>>.Success(hotelsResponse);
        }
    }
}