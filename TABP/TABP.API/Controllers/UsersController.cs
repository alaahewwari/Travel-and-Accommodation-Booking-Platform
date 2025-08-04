using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Users;
using TABP.API.Mappers;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user accounts, such as registration.
    /// </summary>
    [ApiController]
    public class UsersController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">User registration details including name, email, password, and optionally role.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>HTTP 201 Created if registration is successful; otherwise, a relevant error response.</returns>
        /// <response code="201">User registered successfully.</response>
        /// <response code="400">Invalid request or user already exists.</response>
        /// <response code="500">Internal server error occurred during registration.</response>
        [HttpPost(ApiRoutes.Users.Register)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created();
        }
    }

}