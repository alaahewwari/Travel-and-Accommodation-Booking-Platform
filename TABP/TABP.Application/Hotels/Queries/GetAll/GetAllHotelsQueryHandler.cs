using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapper;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Common;
namespace TABP.Application.Hotels.Queries.GetAll
{
    public class GetAllHotelsQueryHandler(
        IHotelRepository hotelRepository
        ) : IRequestHandler<GetAllHotelsQuery, Result<PagedResult<HotelForManagementResponse>>>
    {
        public async Task<Result<PagedResult<HotelForManagementResponse>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            var sieveModel = request.ToSieveModel();
            var hotels = await hotelRepository.GetAllHotelsAsync(sieveModel, cancellationToken);
            var hotelsResponse = hotels.ToHotelForManagementResponse();
            return Result<PagedResult<HotelForManagementResponse>>.Success(hotelsResponse);
        }
    }
}