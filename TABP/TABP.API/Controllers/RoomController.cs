using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Rooms;
using TABP.API.Mapping;
using TABP.Application.Rooms.Commands.Delete;
using TABP.Application.Rooms.Common;
using TABP.Application.Rooms.Queries.GetAll;
using TABP.Application.Rooms.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Handles operations related to hotel rooms, including creation, retrieval, updating, and deletion.
    /// </summary>
    [ApiController]
    public class RoomsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new room under a specific hotel and room class.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel the room belongs to.</param>
        /// <param name="roomClassId">The ID of the room class to associate with the room.</param>
        /// <param name="request">The details of the room to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The newly created room, or an error response if creation fails.</returns>
        [HttpPost(ApiRoutes.Rooms.Create)]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromRoute] long hotelId,
            [FromRoute] long roomClassId,
            [FromBody] CreateRoomRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(hotelId, roomClassId);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }

        /// <summary>
        /// Retrieves a room by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The room details if found, or a 404 error if not found.</returns>
        [HttpGet(ApiRoutes.Rooms.GetById)]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomResponse>> GetById(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var query = new GetRoomByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A list of all available rooms.</returns>
        [HttpGet(ApiRoutes.Rooms.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<RoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllRoomsQuery(), cancellationToken);
            return Ok(result.Value);
        }

        /// <summary>
        /// Updates an existing room's information.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="request">The updated room details.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The updated room data, or an error response if the update fails.</returns>
        [HttpPut(ApiRoutes.Rooms.Update)]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomResponse>> Update(
            [FromRoute] long id,
            [FromBody] UpdateRoomRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if successful, or a 404 error if the room is not found.</returns>
        [HttpDelete(ApiRoutes.Rooms.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoomCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}