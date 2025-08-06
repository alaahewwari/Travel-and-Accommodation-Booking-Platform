using MediatR;
using TABP.Application.Common;
using TABP.Application.Users.Common;
namespace TABP.Application.Users.Queries.GetById
{
    public record GetUserByIdQuery(long Id): IRequest<Result<UserResponse>>;
}