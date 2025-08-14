using MediatR;
using TABP.Application.Common;
using TABP.Application.Reviews.Common;
namespace TABP.Application.Hotels.Queries.GetReviews
{
    public record GetReviewsByHotelQuery(long HotelId) : IRequest<Result<IEnumerable<ReviewResponse>>>;
}