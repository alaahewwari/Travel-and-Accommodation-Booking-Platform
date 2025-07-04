using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
namespace TABP.Application.Hotels.Queries.GetRecentlyVisited
{
    public record GetRecentlyVisitedHotelsQuery(int UserId, int Count) : IRequest<Result<List<HotelResponse>>>;
}