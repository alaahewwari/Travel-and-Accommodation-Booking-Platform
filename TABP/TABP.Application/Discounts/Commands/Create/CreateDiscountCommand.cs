using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
namespace TABP.Application.Discounts.Commands.Create
{
    public record CreateDiscountCommand(long RoomClassId,int Percentage, DateTime StartDate, DateTime EndDate) :IRequest<Result<DiscountResponse>>;
}