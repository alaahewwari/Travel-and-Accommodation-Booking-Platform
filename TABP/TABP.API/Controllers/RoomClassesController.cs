using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.RoomClasses;
using TABP.API.Mapping;
using TABP.Application.RoomClasses.Commands.Delete;
using TABP.Application.RoomClasses.Common;
using TABP.Application.RoomClasses.Queries.GetAll;
using TABP.Application.RoomClasses.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Handles operations related to room classes, including creation, retrieval, updating, and deletion.
    /// </summary>
    [ApiController]
    [OutputCache(Duration = 60)]
    public class RoomClassesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new room class.
        /// </summary>
        /// <param name="request">The details of the room class to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The newly created room class, or an error response if creation fails.</returns>
        [HttpPost(ApiRoutes.RoomClasses.Create)]
        [ProducesResponseType(typeof(RoomClassResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateRoomClassRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }

        /// <summary>
        /// Retrieves a specific room class by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the room class.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The room class details if found, or a 404 error if not found.</returns>
        [HttpGet(ApiRoutes.RoomClasses.GetById)]
        [ProducesResponseType(typeof(RoomClassResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomClassResponse>> GetById([FromRoute] long id, CancellationToken cancellationToken)
        {
            var query = new GetRoomClassByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves all room classes.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A list of all available room classes.</returns>
        [HttpGet(ApiRoutes.RoomClasses.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<RoomClassResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoomClassResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllRoomClassesQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        /// <summary>
        /// Updates the information of an existing room class.
        /// </summary>
        /// <param name="id">The ID of the room class to update.</param>
        /// <param name="request">The updated room class details.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The updated room class, or an error response if the update fails.</returns>
        [HttpPut(ApiRoutes.RoomClasses.Update)]
        [ProducesResponseType(typeof(RoomClassResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomClassResponse>> Update([FromRoute] long id, [FromBody] UpdateRoomClassRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a room class by its ID.
        /// </summary>
        /// <param name="id">The ID of the room class to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if deletion is successful, or a 404 error if the room class is not found.</returns>
        [HttpDelete(ApiRoutes.RoomClasses.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoomClassCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}