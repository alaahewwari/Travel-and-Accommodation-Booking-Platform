using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Hotels;
using TABP.API.Contracts.Images;
using TABP.API.Mappers;
using TABP.Application.Hotels.Commands.Delete;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Queries.GetAll;
using TABP.Application.Hotels.Queries.GetById;
using TABP.Application.Hotels.Queries.GetFeaturedDeals;
using TABP.Application.Hotels.Queries.GetReviews;
using TABP.Application.Reviews.Common;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides API endpoints to manage hotel entities, including creation, retrieval, update, deletion,
    /// featured deals, searching, and image handling.
    /// All endpoints require admin authorization unless otherwise noted.
    /// </summary>
    [ApiController]
    // [OutputCache(Duration = 60)] // Optional: Enable to cache GET responses
    public class HotelsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="request">Hotel data including name, address, city, etc.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The created hotel if successful.</returns>
        /// <response code="201">Hotel created successfully.</response>
        /// <response code="400">Invalid hotel data or validation failed.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User is not authorized (Admin role required).</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(ApiRoutes.Hotels.Create)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }
        /// <summary>
        /// Retrieves a hotel by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the hotel.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The hotel data.</returns>
        /// <response code="200">Hotel found.</response>
        /// <response code="404">Hotel not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(ApiRoutes.Hotels.GetById)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelResponse>> GetById([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetHotelByIdQuery(id), cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Retrieves all hotels.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A list of all hotels.</returns>
        /// <response code="200">List of hotels returned.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(ApiRoutes.Hotels.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<HotelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HotelResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllHotelsQuery(), cancellationToken);
            return Ok(result.Value);
        }
        /// <summary>
        /// Updates an existing hotel.
        /// </summary>
        /// <param name="id">ID of the hotel to update.</param>
        /// <param name="request">Updated hotel data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The updated hotel data.</returns>
        /// <response code="200">Hotel updated successfully.</response>
        /// <response code="400">Invalid update request.</response>
        /// <response code="404">Hotel not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User is not authorized.</response>
        [HttpPut(ApiRoutes.Hotels.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<HotelResponse>> Update([FromRoute] long id, [FromBody] UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        /// <summary>
        /// Deletes a hotel by ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">Hotel deleted.</response>
        /// <response code="404">Hotel not found.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User is not authorized.</response>
        [HttpDelete(ApiRoutes.Hotels.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteHotelCommand(id), cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
        /// <summary>
        /// Retrieves featured hotel deals.
        /// </summary>
        /// <param name="request">Query with number of featured deals to return.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of featured hotels.</returns>
        /// <response code="200">Featured deals returned.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(ApiRoutes.Hotels.GetFeaturedDeals)]
        [ProducesResponseType(typeof(FeaturedDealsHotelsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FeaturedDealsHotelsResponse>> GetFeaturedDeals(
            [FromQuery] HotelFeaturedDealsRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetHotelFeaturedDealsQuery(request.Count), cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Searches for hotels based on filters, dates, guest count, and pagination.
        /// </summary>
        /// <param name="request">Query parameters like city, rating, availability, and page info.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of matching hotels with pagination metadata in headers.</returns>
        /// <response code="200">Search results returned successfully.</response>
        /// <response code="400">Invalid search parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(ApiRoutes.Hotels.Search)]
        [ProducesResponseType(typeof(IEnumerable<HotelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HotelResponse>>> Search(
            [FromQuery] SearchHotelsRequest request,
            CancellationToken cancellationToken)
        {
            var query = request.ToQuery();
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Response.Headers.Append("X-Pagination", result.Value.PaginationMetadata.Build());
            return Ok(result.Value.Items);
        }
        /// <summary>
        /// Sets the thumbnail image for a hotel.
        /// </summary>
        /// <param name="id">Hotel ID to associate the thumbnail with.</param>
        /// <param name="request">Image upload containing the file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>201 Created if successful.</returns>
        /// <response code="201">Thumbnail set successfully.</response>
        /// <response code="400">Invalid or missing image file.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User not authorized.</response>
        [HttpPost(ApiRoutes.Hotels.SetThumbnail)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> SetThumbnail(
            [FromRoute] int id,
            [FromForm] SetImageRequest request,
            CancellationToken cancellationToken)
        {
            using var stream = request.File.OpenReadStream();
            var command = request.ToHotelThumbnailCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created();
        }
        /// <summary>
        /// Adds an image to the hotel's image gallery.
        /// </summary>
        /// <param name="id">Hotel ID to which the image will be added.</param>
        /// <param name="request">Image upload containing the file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>201 Created if successful.</returns>
        /// <response code="201">Image added to gallery successfully.</response>
        /// <response code="400">Invalid image upload request.</response>
        /// <response code="401">Authentication required.</response>
        /// <response code="403">User not authorized.</response>
        [HttpPost(ApiRoutes.Hotels.AddToGallery)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AddToGallery(
            [FromRoute] int id,
            [FromForm] SetImageRequest request,
            CancellationToken cancellationToken)
        {
            using var stream = request.File.OpenReadStream();
            var command = request.ToHotelGalleryCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created();
        }
        /// <summary>
        /// Retrieves all reviews for a specific hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to get reviews for.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A paginated collection of hotel reviews.</returns>
        /// <response code="200">Hotel reviews retrieved successfully.</response>
        /// <response code="400">Invalid query parameters.</response>
        /// <response code="404">Hotel not found.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet(ApiRoutes.Hotels.GetReviews)]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ReviewResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviews(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var query = new GetReviewsByHotelQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}