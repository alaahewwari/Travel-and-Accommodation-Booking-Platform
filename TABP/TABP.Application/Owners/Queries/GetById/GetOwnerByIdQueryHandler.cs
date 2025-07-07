using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
using TABP.Application.Owners.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Owners.Queries.GetById
{
    public class GetOwnerByIdQueryHandler(
        IOwnerRepository ownerRepository
        ):IRequestHandler<GetOwnerByIdQuery, Result<OwnerResponse>>
    {
        public async Task<Result<OwnerResponse>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await ownerRepository.GetOwnerByIdAsync(request.Id, cancellationToken);
            if (owner is null)
                return Result<OwnerResponse>.Failure(OwnerErrors.OwnerNotFound);
            var response = owner.ToOwnerResponse();
            return Result<OwnerResponse>.Success(response);
        }
    }
}