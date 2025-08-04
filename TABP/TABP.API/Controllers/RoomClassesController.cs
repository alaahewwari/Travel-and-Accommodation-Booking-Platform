using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Amenities;
using TABP.API.Contracts.Images;
using TABP.API.Contracts.RoomClasses;
using TABP.API.Mappers;
using TABP.Application.Discounts.Common;
using TABP.Application.RoomClasses.Commands.AddAmenityToRoomClass;
using TABP.Application.RoomClasses.Commands.Delete;
using TABP.Application.RoomClasses.Common;
using TABP.Application.RoomClasses.Queries.GetAll;
using TABP.Application.RoomClasses.Queries.GetById;
using TABP.Application.RoomClasses.Queries.GetByRoomClass;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing room classes, including creation, retrieval,
    /// updates, deletion, image uploads, amenity assignment, and discount retrieval.
    /// </summary>
    [ApiController]
    // [OutputCache(Duration = 60)] // Uncomment to enable response caching
    public class RoomClassesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new room class.
        /// </summary>
        /// <param name="request">Details of the room class to be created.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The created room class.</returns>
        /// <response code="201">Room class created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User lacks admin privileges.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(ApiRoutes.RoomClasses.Create)]
        [Authorize(Roles = UserRoles.Admin)]
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
        /// Retrieves a room class by its ID.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The requested room class.</returns>
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
        /// Retrieves all available room classes.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>List of room classes.</returns>
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
        /// Updates an existing room class.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="request">Updated room class data.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated room class.</returns>
        [HttpPut(ApiRoutes.RoomClasses.Update)]
        [Authorize(Roles = UserRoles.Admin)]
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
        /// Deletes a room class by ID.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete(ApiRoutes.RoomClasses.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
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
        /// <summary>
        /// Uploads and sets the thumbnail image for a room class.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="request">Image upload request.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>201 Created on success.</returns>
        [HttpPost(ApiRoutes.RoomClasses.SetThumbnail)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SetThumbnail([FromRoute] int id, [FromForm] SetImageRequest request, CancellationToken cancellationToken)
        {
            if (request.File is null || request.File.Length == 0)
                return BadRequest("Image file is required.");

            using var stream = request.File.OpenReadStream();
            var command = request.ToRoomThumbnailCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created();
        }
        /// <summary>
        /// Adds an image to the room class gallery.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="request">Image upload request.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>201 Created on success.</returns>
        [HttpPost(ApiRoutes.RoomClasses.AddToGallery)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddToGallery([FromRoute] int id, [FromForm] SetImageRequest request, CancellationToken cancellationToken)
        {
            if (request.File is null || request.File.Length == 0)
                return BadRequest("File is missing or empty.");

            using var stream = request.File.OpenReadStream();
            var command = request.ToRoomGalleryCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created();
        }
        /// <summary>
        /// Assigns an amenity to a room class.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="request">Amenity assignment request.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>No content on success.</returns>
        [HttpPost(ApiRoutes.RoomClasses.AddAmenity)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAmenity(
            [FromRoute] long id,
            [FromBody] AssignAmenityToRoomClassRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddAmenityToRoomClassCommand(id, request.AmenityId);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }
        /// <summary>
        /// Retrieves all active discounts assigned to a specific room class.
        /// </summary>
        /// <param name="id">Room class ID.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The discount details.</returns>
        [HttpGet(ApiRoutes.RoomClasses.GetDiscount)]
        [ProducesResponseType(typeof(DiscountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DiscountResponse>> GetDiscount(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var query = new GetDiscountByRoomClassQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
    }
}
