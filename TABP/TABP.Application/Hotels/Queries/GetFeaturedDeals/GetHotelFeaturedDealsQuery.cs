using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
namespace TABP.Application.Hotels.Queries.GetFeaturedDeals
{
    public record GetHotelFeaturedDealsQuery(int Count) : IRequest<Result<IEnumerable<HotelFeaturedDealsResponse>>>;
}