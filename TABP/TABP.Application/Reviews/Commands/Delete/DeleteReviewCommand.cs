using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Reviews.Commands.Delete
{
    public record DeleteReviewCommand(long Id): IRequest<Result>;
}