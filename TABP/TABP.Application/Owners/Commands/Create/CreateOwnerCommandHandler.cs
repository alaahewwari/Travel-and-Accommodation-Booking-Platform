using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
using TABP.Application.Owners.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Owners.Commands.Create
{
    public class CreateOwnerCommandHandler(IOwnerRepository ownerRepository) : IRequestHandler<CreateOwnerCommand, Result<OwnerResponse>>
    {
        public async Task<Result<OwnerResponse>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var ownerModel = request.ToOwnerDomain();
            var owner = await ownerRepository.CreateOwnerAsync(ownerModel, cancellationToken);
            var response = owner.ToOwnerResponse();
            return Result<OwnerResponse>.Success(response);
        }
    }
}