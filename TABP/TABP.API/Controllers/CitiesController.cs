using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.Cities;
using TABP.API.Mapping;
using TABP.Application.Cities.Commands.Delete;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Queries.GetAll;
using TABP.Application.Cities.Queries.GetById;
using TABP.Application.Cities.Queries.GetTrending;
using TABP.Application.Hotels.Common;
namespace TABP.API.Controllers
{
    [ApiController]
    [OutputCache(Duration = 60)]
    public class CitiesController(ISender mediator, CityMapper mapper) : ControllerBase
    {
        [HttpPost(ApiRoutes.Cities.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityResponse>> Create([FromBody] CreateCityRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }
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
            {
                return NotFound(result.Error.Description);
            }
            return Ok(result.Value);
        }
        [HttpGet(ApiRoutes.Cities.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<CityResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllCitiesQuery(), cancellationToken);
            return Ok(result.Value);
        }
        [HttpPut(ApiRoutes.Cities.Update)]
        [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityResponse>> Update([FromRoute] int id, [FromBody] UpdateCityRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request, id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return Ok(result.Value);
        }
        [HttpDelete(ApiRoutes.Cities.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteCityCommand(id), cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error.Description);
            }
            return NoContent();
        }
        [HttpGet(ApiRoutes.Cities.GetTrending)]
        [ProducesResponseType(typeof(IEnumerable<CityResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityResponse>>> GetTrending([FromQuery] TrendingCitiesRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetTrendingCitiesQuery(request.Count);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error.Description);
            return Ok(result.Value);
        }
    }
}