using MediatR;
using TABP.Application.Common;
using TABP.Application.Reviews.Common;
namespace TABP.Application.Reviews.Commands.Create
{
    public record CreateReviewCommand(
        long HotelId,
        int Rating,
        string? Comment
        ): IRequest<Result<ReviewResponse>>;
}