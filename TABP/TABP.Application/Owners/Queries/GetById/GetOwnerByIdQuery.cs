using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
namespace TABP.Application.Owners.Queries.GetById
{
    public record GetOwnerByIdQuery(long Id): IRequest<Result<OwnerResponse>>;
}