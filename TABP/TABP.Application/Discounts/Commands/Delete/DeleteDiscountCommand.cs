using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Discounts.Commands.Delete
{
    public record DeleteDiscountCommand(int Id):IRequest<Result>;
}