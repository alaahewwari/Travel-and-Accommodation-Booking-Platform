using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
namespace TABP.Application.RoomClasses.Queries.GetByRoomClass
{
    public record GetDiscountByRoomClassQuery(long id) : IRequest<Result<DiscountResponse>>;
}