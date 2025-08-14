using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Cities;
using TABP.API.Contracts.Images;
using TABP.API.Mappers;
using TABP.Application.Cities.Commands.Delete;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Queries.GetAll;
using TABP.Application.Cities.Queries.GetById;
using TABP.Application.Cities.Queries.GetTrending;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing cities including creation, retrieval, update, deletion,
    /// setting city thumbnails, and fetching trending cities.
    /// </summary>
    [ApiController]
    // [OutputCache(Duration = 60)] // Enable if caching is needed
    public class CitiesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new city. Only accessible by administrators.
        /// </summary>
        /// <param name="request">The request body containing city name and optional metadata.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The created city details if successful.</returns>
        /// <response code="201">City created successfully.</response>
        /// <response code="400">Invalid request body or business validation failed.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks admin privileges.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost(ApiRoutes.Cities.Create)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(CityResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityResponse>> Create([FromBody] CreateCityRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Retrieves a city by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the city to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The city details if found.</returns>
        /// <response code="200">City found and returned.</response>
        /// <response code="404">City not found.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks permission to access this resource.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet(ApiRoutes.Cities.GetById)]
        [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetCityByIdQuery(id), cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves a list of all available cities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A collection of all cities.</returns>
        /// <response code="200">Cities retrieved successfully.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks permission to access this resource.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet(ApiRoutes.Cities.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<CityResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllCitiesQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result.Value);
        }

        /// <summary>
        /// Updates an existing city. Only accessible by administrators.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="request">The updated city data.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The updated city details if successful.</returns>
        /// <response code="200">City updated successfully.</response>
        /// <response code="400">Invalid request body or business rule violation.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks admin privileges.</response>
        /// <response code="404">City not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut(ApiRoutes.Cities.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityResponse>> Update([FromRoute] int id, [FromBody] UpdateCityRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a city by its ID. Only accessible by administrators.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>No content if deletion is successful.</returns>
        /// <response code="204">City deleted successfully.</response>
        /// <response code="404">City not found.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks admin privileges.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete(ApiRoutes.Cities.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteCityCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of trending cities based on popularity or recent activity.
        /// </summary>
        /// <param name="request">The request specifying how many trending cities to return.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A list of trending cities.</returns>
        /// <response code="200">Trending cities retrieved successfully.</response>
        /// <response code="400">Invalid count or query parameters.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet(ApiRoutes.Cities.GetTrending)]
        [ProducesResponseType(typeof(IEnumerable<CityResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityResponse>>> GetTrending([FromQuery] GetTrendingCitiesRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetTrendingCitiesQuery(request.Count);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Sets or updates the thumbnail image for a specific city.
        /// Only accessible by administrators.
        /// </summary>
        /// <param name="id">The ID of the city to associate the thumbnail with.</param>
        /// <param name="request">The image upload request containing the file.</param>
        /// <param name="cancellationToken">A cancellation token to abort the request if needed.</param>
        /// <returns>
        /// 201 Created if the image is uploaded successfully; otherwise, a 400 Bad Request.
        /// </returns>
        /// <response code="201">Thumbnail successfully set for the city.</response>
        /// <response code="400">Invalid or missing image file.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks admin privileges.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost(ApiRoutes.Cities.SetThumbnail)]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetThumbnail([FromRoute] int id, [FromForm] SetImageRequest request, CancellationToken cancellationToken)
        {
            using var stream = request.File.OpenReadStream();
            var command = request.ToCityCommand(id, stream, request.File.FileName);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created();
        }
    }
}