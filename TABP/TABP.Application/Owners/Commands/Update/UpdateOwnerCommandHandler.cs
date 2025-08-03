using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
using TABP.Application.Owners.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Owners.Commands.Update
{
    public class UpdateOwnerCommandHandler(
        IOwnerRepository ownerRepository)
        : IRequestHandler<UpdateOwnerCommand, Result<OwnerResponse>>
    {
        public async Task<Result<OwnerResponse>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var existingOwner = await ownerRepository.GetOwnerByIdAsync(request.Id, cancellationToken);
            if (existingOwner is null)
            {
                return Result<OwnerResponse>.Failure(OwnerErrors.OwnerNotFound);
            }
            var ownerModel = request.ToOwnerDomain();
            var updatedOwner = await ownerRepository.UpdateOwnerAsync(ownerModel, cancellationToken);
            if (updatedOwner is null)
            {
                return Result<OwnerResponse>.Failure(OwnerErrors.NotModified);
            }
            var owner = updatedOwner.ToOwnerResponse();
            return Result<OwnerResponse>.Success(owner);
        }
    }
}