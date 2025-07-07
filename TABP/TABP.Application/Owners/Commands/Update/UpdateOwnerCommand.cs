using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
namespace TABP.Application.Owners.Commands.Update
{
    public record UpdateOwnerCommand(int Id, string FirstName, string LastName, string PhoneNumber) : IRequest<Result<OwnerResponse>>;
}