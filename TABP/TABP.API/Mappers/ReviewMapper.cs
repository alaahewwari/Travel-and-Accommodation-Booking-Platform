using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Reviews;
using TABP.Application.Reviews.Commands.Create;
using TABP.Application.Reviews.Commands.Update;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class ReviewMapper
    {
        public static partial CreateReviewCommand ToCommand(this CreateReviewRequest request);
        public static partial UpdateReviewCommand ToCommand(this UpdateReviewRequest request, long id);
    }
}