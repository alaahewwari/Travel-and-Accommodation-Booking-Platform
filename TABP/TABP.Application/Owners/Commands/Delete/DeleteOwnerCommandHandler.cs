using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Owners.Commands.Delete
{
    public class DeleteOwnerCommandHandler(IOwnerRepository ownerRepository)
         : IRequestHandler<DeleteOwnerCommand, Result>
    {
        public async Task<Result> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await ownerRepository.GetOwnerByIdAsync(request.Id, cancellationToken);
            if (owner is null)
                return Result.Failure(OwnerErrors.OwnerNotFound);
            await ownerRepository.DeleteOwnerAsync(owner.Id, cancellationToken);
            return Result.Success();
        }
    }
}