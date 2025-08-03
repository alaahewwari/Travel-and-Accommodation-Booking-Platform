using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
namespace TABP.Application.Discounts.Queries.GetByRoomClass
{
    public record GetDiscountByRoomClassQuery(long roomCLassId) : IRequest<Result<DiscountResponse>>;
}