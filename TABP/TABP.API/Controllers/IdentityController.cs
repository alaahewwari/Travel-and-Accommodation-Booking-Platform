using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Users;
using TABP.API.Mapping;
using TABP.Application.Users.Common;
namespace TABP.API.Controllers
{
    [ApiController]
    public class IdentityController(ISender mediator, IdentityMapper mapper) : ControllerBase
    {
        [HttpPost(ApiRoutes.Identity.Login)]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return Ok(result.Value);
        }
    }
}