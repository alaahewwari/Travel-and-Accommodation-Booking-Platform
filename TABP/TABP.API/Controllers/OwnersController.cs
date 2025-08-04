using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Owners;
using TABP.API.Mappers;
using TABP.Application.Owners.Commands.Delete;
using TABP.Application.Owners.Common;
using TABP.Application.Owners.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing hotel owners, including creation, retrieval,
    /// updating, and deletion operations.
    /// </summary>
    [ApiController]
    public class OwnersController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new hotel owner.
        /// </summary>
        /// <param name="request">The data required to create an owner.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The created owner data.</returns>
        /// <response code="201">Owner created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(ApiRoutes.Owners.Create)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(OwnerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerResponse>> Create(
            [FromBody] CreateOwnerRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }
        /// <summary>
        /// Retrieves a hotel owner by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the owner.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The owner details if found.</returns>
        /// <response code="200">Owner found and returned.</response>
        /// <response code="404">Owner not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(ApiRoutes.Owners.GetById)]
        [ProducesResponseType(typeof(OwnerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerResponse>> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var query = new GetOwnerByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        /// <summary>
        /// Updates the details of an existing hotel owner.
        /// </summary>
        /// <param name="id">The ID of the owner to update.</param>
        /// <param name="request">The updated owner data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The updated owner details.</returns>
        /// <response code="200">Owner updated successfully.</response>
        /// <response code="400">Invalid update data.</response>
        /// <response code="404">Owner not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut(ApiRoutes.Owners.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(OwnerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerResponse>> Update(
            [FromRoute] int id,
            [FromBody] UpdateOwnerRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Deletes a hotel owner by ID.
        /// </summary>
        /// <param name="id">The ID of the owner to delete.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>No content on successful deletion.</returns>
        /// <response code="204">Owner deleted successfully.</response>
        /// <response code="404">Owner not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete(ApiRoutes.Owners.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteOwnerCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}