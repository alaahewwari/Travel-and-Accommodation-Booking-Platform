using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
using TABP.Application.Discounts.Mappers;
using TABP.Application.RoomClassClasses.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.RoomClasses.Queries.GetByRoomClass
{
    public class GetDiscountByRoomClassQueryHandler(
        IRoomClassRepository roomClassRepository
        ) : IRequestHandler<GetDiscountByRoomClassQuery, Result<DiscountResponse>>
    {
        public async Task<Result<DiscountResponse>> Handle(GetDiscountByRoomClassQuery request, CancellationToken cancellationToken)
        {
            var existingRoomClass = await roomClassRepository.GetRoomClassByIdAsync(request.id, cancellationToken);
            if (existingRoomClass is null)
            {
                return Result<DiscountResponse>.Failure(RoomClassErrors.RoomClassNotFound);
            }
            var discount = await roomClassRepository.GetDiscountByRoomClassAsync(request.id, cancellationToken);
            if (discount is null)
            {
                return Result<DiscountResponse>.Failure(DiscountErrors.DiscountNotFound);
            }
            var response = discount.ToDiscountResponse();
            return Result<DiscountResponse>.Success(response);
        }
    }
}