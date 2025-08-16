using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.Amenities;
using TABP.API.Mappers;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Queries.GetAll;
using TABP.Application.Amenities.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Exposes endpoints for managing hotel amenities, including creation, retrieval, and updates.
    /// Amenities are used to describe features or services available in rooms or hotel facilities.
    /// </summary>
    [ApiController]
    [OutputCache(Duration = 60)]
    public class AmenitiesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new amenity.
        /// </summary>
        /// <param name="request">The amenity details including name and description.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The newly created amenity.</returns>
        /// <response code="201">Amenity created successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User does not have admin privileges.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(ApiRoutes.Amenities.Create)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Retrieves a list of all amenities available in the system.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of amenities.</returns>
        /// <response code="200">Amenities retrieved successfully.</response>
        /// <response code="500">Unexpected error retrieving amenities.</response>
        [HttpGet(ApiRoutes.Amenities.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<AmenityResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllAmenitiesQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves details of a specific amenity by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the amenity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The amenity details.</returns>
        /// <response code="200">Amenity found and returned.</response>
        /// <response code="404">Amenity not found.</response>
        [HttpGet(ApiRoutes.Amenities.GetById)]
        [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAmenityByIdQuery(id), cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Updates an existing amenity (Admin only).
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="request">Updated data for the amenity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated amenity.</returns>
        /// <response code="200">Amenity updated successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User does not have admin privileges.</response>
        /// <response code="404">Amenity not found.</response>
        [HttpPut(ApiRoutes.Amenities.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AmenityResponse>> Update(
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
    }
}