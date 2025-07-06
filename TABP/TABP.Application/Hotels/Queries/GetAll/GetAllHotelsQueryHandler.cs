using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Hotels.Queries.GetAll
{
    public class GetAllHotelsQueryHandler(
        IHotelRepository hotelRepository
        ) : IRequestHandler<GetAllHotelsQuery, Result<IEnumerable<HotelForManagementResponse>>>
    {
        public async Task<Result<IEnumerable<HotelForManagementResponse>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await hotelRepository.GetAllHotelsAsync(cancellationToken);
            var hotelsResponse = hotels.Select(h=>h.ToHotelForManagementResponse());
            return Result<IEnumerable<HotelForManagementResponse>>.Success(hotelsResponse);
        }
    }
}