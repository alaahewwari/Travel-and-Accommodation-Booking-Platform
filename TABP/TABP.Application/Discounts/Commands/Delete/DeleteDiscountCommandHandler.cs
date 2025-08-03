using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Discounts.Commands.Delete
{
    public class DeleteDiscountCommandHandler(IDiscountRepository discountRespository) : IRequestHandler<DeleteDiscountCommand, Result>
    {
        public async Task<Result> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount= await discountRespository.GetDiscountByIdAsync(request.Id,cancellationToken);
            if (discount == null)
            {
                return Result.Failure(DiscountErrors.DiscountNotFound);
            }
            await discountRespository.DeleteDiscountAsync(discount.Id, cancellationToken);
            return Result.Success();
        }
    }
}