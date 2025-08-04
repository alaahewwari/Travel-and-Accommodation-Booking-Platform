using MediatR;
using TABP.Application.Common;
using TABP.Application.Reviews.Common;
namespace TABP.Application.Reviews.Queries.GetById
{
    public record GetReviewByIdQuery(long Id) : IRequest<Result<ReviewResponse>>;
}