using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Users;
using TABP.API.Mappers;
using TABP.Application.Users.Common;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Handles user authentication operations such as login.
    /// Provides endpoints for obtaining JWT tokens upon successful authentication.
    /// </summary>
    [ApiController]
    public class AuthenticationController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Authenticates a user using email and password, and returns a JWT access token if the credentials are valid.
        /// </summary>
        /// <param name="request">Contains the user credentials (email and password).</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>
        /// A JWT token and basic user information if login is successful.
        /// </returns>
        /// <response code="200">Login successful; JWT token returned.</response>
        /// <response code="400">Invalid login request (e.g., missing fields).</response>
        /// <response code="401">Authentication failed due to incorrect credentials.</response>
        /// <response code="500">Internal server error during authentication.</response>
        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginUserResponse>> Login(
            [FromBody] LoginUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
    }
}