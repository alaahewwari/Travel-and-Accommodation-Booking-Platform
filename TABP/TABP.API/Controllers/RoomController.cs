using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.Rooms;
using TABP.API.Mappers;
using TABP.Application.Rooms.Commands.Delete;
using TABP.Application.Rooms.Common;
using TABP.Application.Rooms.Queries.GetAll;
using TABP.Application.Rooms.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing hotel rooms, including creation, retrieval,
    /// updates, and deletion. All modifying actions require admin privileges.
    /// </summary>
    [ApiController]
    [OutputCache(Duration = 60)]
    public class RoomsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new room under a specific hotel and room class.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel the room will belong to.</param>
        /// <param name="roomClassId">The ID of the room class to associate with the room.</param>
        /// <param name="request">Details of the room to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The created room.</returns>
        /// <response code="201">Room created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(ApiRoutes.Rooms.Create)]
        [Authorize(Roles = UserRoles.Admin)]
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
        /// Retrieves a room by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The room details.</returns>
        /// <response code="200">Room found and returned.</response>
        /// <response code="404">Room not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Access forbidden.</response>
        /// <response code="500">Internal server error.</response>
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
        /// Retrieves a list of all rooms in the system.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>List of all rooms.</returns>
        /// <response code="200">Rooms retrieved successfully.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Access forbidden.</response>
        /// <response code="500">Internal server error.</response>
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
        /// Updates the details of an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="request">The updated room information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated room data.</returns>
        /// <response code="200">Room updated successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="404">Room not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut(ApiRoutes.Rooms.Update)]
        [Authorize(Roles = UserRoles.Admin)]
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
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>No content if deletion is successful.</returns>
        /// <response code="204">Room deleted successfully.</response>
        /// <response code="404">Room not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">Admin privileges required.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete(ApiRoutes.Rooms.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteRoomCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}