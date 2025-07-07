using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
using TABP.Application.Discounts.Mappers;
using TABP.Application.RoomClassClasses.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Discounts.Queries.GetByRoomClass
{
    public class GetDiscountByRoomClassQueryHandler(
        IDiscountRespository discountRespository,
        IRoomClassRepository roomClassRepository
        ) : IRequestHandler<GetDiscountByRoomClassQuery, Result<DiscountResponse>>
    {
        public async Task<Result<DiscountResponse>> Handle(GetDiscountByRoomClassQuery request, CancellationToken cancellationToken)
        {
            var existingRoomClass = await roomClassRepository.GetRoomClassByIdAsync(request.roomCLassId, cancellationToken);
            if (existingRoomClass is null)
            {
                return Result<DiscountResponse>.Failure(RoomClassErrors.RoomClassNotFound);
            }
            var discount = await discountRespository.GetDiscountByRoomClassAsync(request.roomCLassId, cancellationToken);
            var response = discount.ToDiscountResponse();
            return Result<DiscountResponse>.Success(response);
        }
    }
}