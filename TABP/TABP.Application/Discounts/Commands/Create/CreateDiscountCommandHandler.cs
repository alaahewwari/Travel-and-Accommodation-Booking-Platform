using MediatR;
using TABP.Application.Common;
using TABP.Application.Discounts.Common;
using TABP.Application.Discounts.Mappers;
using TABP.Application.RoomClassClasses.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Discounts.Commands.Create
{
    public class CreateDiscountCommandHandler(
        IDiscountRepository discountRespository,
        IRoomClassRepository roomClassRepository
        ) : IRequestHandler<CreateDiscountCommand, Result<DiscountResponse>>
    {
        public async Task<Result<DiscountResponse>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var roomClass = await roomClassRepository.GetRoomClassByIdAsync(request.RoomClassId, cancellationToken);
            if (roomClass == null)
            {
                return Result<DiscountResponse>.Failure(RoomClassErrors.RoomClassNotFound);
            }
            var discount = request.ToDiscountDomain();
            var createdDiscount = await discountRespository.CreateAndAssignDiscountAsync(discount, roomClass, cancellationToken);
            var response = createdDiscount.ToDiscountResponse();
            return Result<DiscountResponse>.Success(response);
        }
    }
}