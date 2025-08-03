using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics;
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
    /// Manages city operations including creation, retrieval, updating, deletion,
    /// and fetching trending cities.
    /// </summary>
    [ApiController]
    //[OutputCache(Duration = 60)]
    public class CitiesController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new city.
        /// </summary>
        /// <param name="request">The city details to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The newly created city, or an error response if creation fails.</returns>
        [HttpPost(ApiRoutes.Cities.Create)]
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
        /// Retrieves a specific city by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The city details if found, or a 404 error if not found.</returns>
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
        /// Retrieves all cities.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A list of all available cities.</returns>
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
        /// Updates an existing city.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="request">The updated city data.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The updated city, or an error response if the update fails.</returns>
        [HttpPut(ApiRoutes.Cities.Update)]
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
        /// Deletes a city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if the deletion is successful, or a 404 error if the city is not found.</returns>
        [HttpDelete(ApiRoutes.Cities.Delete)]
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
        /// Retrieves a list of trending cities.
        /// </summary>
        /// <param name="request">The request specifying how many trending cities to return.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A collection of trending cities.</returns>
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
        /// Sets the thumbnail image for a specific entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to associate the thumbnail with.</param>
        /// <param name="request">The image upload request containing the file.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <c>201 Created</c> if the thumbnail is successfully set;
        /// otherwise, returns <c>400 Bad Request</c> with error details.
        /// </returns>
        [HttpPost(ApiRoutes.Cities.SetThumbnail)]
        public async Task<ActionResult> SetThumbnail([FromRoute] int id, [FromForm] SetImageRequest request, CancellationToken cancellationToken)
        {
            if (request.File is null || request.File.Length == 0)
            {
                return BadRequest();
            }
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