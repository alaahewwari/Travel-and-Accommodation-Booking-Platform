using MediatR;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Amenities;
using TABP.API.Mapping;
using TABP.Application.Amenities.Commands.AssignToRoomClass;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Queries.GetAll;
using TABP.Application.Amenities.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Manages hotel amenities including creation, retrieval, updates, and room class assignments.
    /// </summary>
    [ApiController]
    [Route(ApiRoutes.Base)]
    [Produces("application/json")]
    [Tags("Amenities")]
    public class AmenitiesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new amenity.
        /// </summary>
        /// <param name="request">The amenity creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created amenity.</returns>
        /// <response code="201">Amenity created successfully.</response>
        /// <response code="400">Invalid request data.</response>
        [HttpPost(ApiRoutes.Amenities.Create)]
        [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateAmenityRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }
        /// <summary>
        /// Retrieves all amenities in the system.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of all amenities.</returns>
        /// <response code="200">Returns all amenities.</response>
        [HttpGet(ApiRoutes.Amenities.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<AmenityResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllAmenitiesQuery();
            var result = await mediator.Send(query, cancellationToken);

            if (result.IsFailure)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Error);

            return Ok(result.Value);
        }
        /// <summary>
        /// Retrieves a specific amenity by ID.
        /// </summary>
        /// <param name="id">The amenity ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The amenity details.</returns>
        /// <response code="200">Returns the amenity.</response>
        /// <response code="404">Amenity not found.</response>
        [HttpGet(ApiRoutes.Amenities.GetById)]
        [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAmenityByIdQuery(id), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
        /// <summary>
        /// Updates an existing amenity.
        /// </summary>
        /// <param name="id">The amenity ID.</param>
        /// <param name="request">The update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated amenity.</returns>
        /// <response code="200">Amenity updated successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="404">Amenity not found.</response>
        [HttpPut(ApiRoutes.Amenities.Update)]
        [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateAmenityRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        /// <summary>
        /// Assigns an amenity to a room class.
        /// </summary>
        /// <param name="id">The amenity ID.</param>
        /// <param name="request">The assignment request containing room class ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">Assignment successful.</response>
        /// <response code="404">Amenity or room class not found.</response>
        [HttpPost(ApiRoutes.Amenities.AssignToRoomClass)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignAmenityToRoomClass(
            long id,
            [FromBody] AssignAmenityToRoomClassRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AssignAmenityToRoomClassCommand(id, request.RoomClassId);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}