using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Users;
using TABP.API.Mapping;
using TABP.Application.Users.Common;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Handles user authentication, including login functionality.
    /// </summary>
    [ApiController]
    public class IdentityController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Authenticates a user using their credentials and returns an access token upon success.
        /// </summary>
        /// <param name="request">The login credentials, including username and password.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A login response containing user info and token, or an error if authentication fails.</returns>
        [HttpPost(ApiRoutes.Identity.Login)]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}