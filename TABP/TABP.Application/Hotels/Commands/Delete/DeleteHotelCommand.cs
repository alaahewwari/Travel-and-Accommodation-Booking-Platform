using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Hotels.Commands.Delete
{
    public record DeleteHotelCommand(long Id): IRequest<Result>;
}