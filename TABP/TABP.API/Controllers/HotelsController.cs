using MediatR;
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
namespace TABP.API.Controllers
{
    /// <summary>
    /// Manages hotel operations including creation, retrieval, updating, deletion,
    /// and fetching featured hotel deals.
    /// </summary>
    [ApiController]
    //[OutputCache(Duration = 60)]
    public class HotelsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="request">The hotel details to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The newly created hotel, or an error response if creation fails.</returns>
        [HttpPost(ApiRoutes.Hotels.Create)]
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
        /// Retrieves a hotel by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The hotel data if found, or a 404 error if not found.</returns>
        [HttpGet(ApiRoutes.Hotels.GetById)]
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
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A list of all available hotels.</returns>
        [HttpGet(ApiRoutes.Hotels.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<HotelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HotelResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllHotelsQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result.Value);
        }
        /// <summary>
        /// Updates the information of an existing hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="request">The updated hotel data.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The updated hotel, or an error response if the update fails.</returns>
        [HttpPut(ApiRoutes.Hotels.Update)]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelResponse>> Update([FromRoute] long id, [FromBody] UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Deletes a hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if the hotel is successfully deleted, or a 404 error if not found.</returns>
        [HttpDelete(ApiRoutes.Hotels.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new DeleteHotelCommand(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
        /// <summary>
        /// Retrieves a list of featured hotel deals.
        /// </summary>
        /// <param name="request">The request specifying how many featured deals to return.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A collection of featured hotel deals.</returns>
        [HttpGet(ApiRoutes.Hotels.GetFeaturedDeals)]
        [ProducesResponseType(typeof(FeaturedDealsHotelsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FeaturedDealsHotelsResponse>> GetFeaturedDeals(
            [FromQuery] HotelFeaturedDealsRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = new GetHotelFeaturedDealsQuery(request.Count);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Searches for hotels based on filters, sorts, dates, guests, and pagination.
        /// Returns results with pagination metadata in the response headers.
        /// </summary>
        /// <param name="request">Query parameters including filters, page, size, etc.</param>
        /// <param name="cancellationToken">Token to cancel the request if needed.</param>
        /// <returns>A list of matching hotels with pagination info in headers.</returns>
        [HttpGet(ApiRoutes.Hotels.Search)]
        public async Task<ActionResult<IEnumerable<HotelResponse>>> Search([FromQuery] SearchHotelsRequest request, CancellationToken cancellationToken)
        {
            var query = request.ToQuery();
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            Response.Headers.Append("X-Pagination", result.Value.PaginationMetadata.Build());
            return Ok(result.Value.Items);
        }
        /// <summary>
        /// Sets the thumbnail image for a specific hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to associate the thumbnail with.</param>
        /// <param name="request">The image upload request containing the file.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <c>201 Created</c> if the thumbnail is successfully set;
        /// otherwise, returns <c>400 Bad Request</c> with error details.
        /// </returns>
        [HttpPost(ApiRoutes.Hotels.SetThumbnail)]
        public async Task<ActionResult> SetThumbnail([FromRoute] int id, [FromForm] SetImageRequest request, CancellationToken cancellationToken)
        {
            if (request.File is null || request.File.Length == 0)
            {
                return BadRequest();
            }
            using var stream = request.File.OpenReadStream();
            var command = request.ToHotelThumbnailCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created();
        }
        /// <summary>
        /// Adds a new image to the gallery of a specific hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to which the image will be added.</param>
        /// <param name="request">The image upload request containing the file to add.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>Returns 201 Created if the image is successfully added, or 400 BadRequest if the input is invalid or the operation fails.</returns>
        [HttpPost(ApiRoutes.Hotels.AddToGallery)]
        public async Task<ActionResult> AddToGallery([FromRoute] int id, [FromForm] SetImageRequest request, CancellationToken cancellationToken)
        {
            if (request.File is null || request.File.Length == 0)
            {
                return BadRequest("File is missing or empty.");
            }

            using var stream = request.File.OpenReadStream();
            var command = request.ToHotelGalleryCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created();
        }
    }
}