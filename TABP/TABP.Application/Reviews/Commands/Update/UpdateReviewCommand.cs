using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Reviews.Commands.Update
{
    public record UpdateReviewCommand(
        long Id,
        long HotelId,
        int Rating,
        string? Comment) :IRequest<Result>;
}