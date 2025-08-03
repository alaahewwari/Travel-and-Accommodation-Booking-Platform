using MediatR;
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
    /// Manages operations related to hotel owners, including creation, retrieval, updating, and deletion.
    /// </summary>
    [ApiController]
    public class OwnersController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="request">The details of the owner to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The newly created owner data, or an error if creation fails.</returns>
        [HttpPost(ApiRoutes.Owners.Create)]
        [ProducesResponseType(typeof(OwnerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerResponse>> Create([FromBody] CreateOwnerRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }
        /// <summary>
        /// Retrieves an owner by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the owner.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The owner data if found, or a 404 error if not found.</returns>
        [HttpGet(ApiRoutes.Owners.GetById)]
        [ProducesResponseType(typeof(OwnerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetOwnerByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Updates an existing owner's information.
        /// </summary>
        /// <param name="id">The ID of the owner to update.</param>
        /// <param name="request">The updated owner details.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The updated owner data, or an error if the update fails.</returns>
        [HttpPut(ApiRoutes.Owners.Update)]
        [ProducesResponseType(typeof(OwnerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Deletes an owner by their ID.
        /// </summary>
        /// <param name="id">The ID of the owner to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if successful, or a 404 error if the owner is not found.</returns>
        [HttpDelete(ApiRoutes.Owners.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteOwnerCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}