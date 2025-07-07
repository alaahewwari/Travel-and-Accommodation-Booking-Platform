using MediatR;
using TABP.Application.Common;
using TABP.Application.Owners.Common;
namespace TABP.Application.Owners.Commands.Create
{
    public record CreateOwnerCommand(string FirstName, string LastName, string PhoneNumber) : IRequest<Result<OwnerResponse>>;
}