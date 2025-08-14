using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
namespace TABP.Application.Hotels.Queries.GetById
{
    public record GetHotelByIdQuery(long Id) : IRequest<Result<HotelResponse>>;
}