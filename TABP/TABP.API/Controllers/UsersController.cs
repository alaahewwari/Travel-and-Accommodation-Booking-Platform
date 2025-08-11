using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Users;
using TABP.API.Mappers;
using TABP.Application.Users.Common;
using TABP.Application.Users.Queries.GetById;
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
        public async Task<ActionResult<UserResponse>> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>HTTP 200 OK with user details if found; otherwise, HTTP 404 Not Found.</returns>
        /// <response code="200">User retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error occurred during retrieval.</response>
        [HttpGet(ApiRoutes.Users.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> GetById(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}