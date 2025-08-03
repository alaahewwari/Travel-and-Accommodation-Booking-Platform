using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Users;
using TABP.API.Mappers;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Handles user authentication, including login functionality.
    /// </summary>
    [ApiController]
    public class UsersController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">The registration details including username, email, and password.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>Created response</returns>
        [HttpPost(ApiRoutes.Users.Register)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created();
        }
    }
}