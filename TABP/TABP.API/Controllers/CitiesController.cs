using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.City;
using TABP.API.Mapping;
using TABP.Application.Cities.Commands.Delete;
using TABP.Application.Cities.Queries.GetAll;
using TABP.Application.Cities.Queries.GetById;
namespace TABP.API.Controllers
{
    [ApiController]
    [OutputCache(Duration = 60)]
    public class CitiesController(ISender mediator, CityMapper mapper) : ControllerBase
    {
        [HttpPost(ApiRoutes.Cities.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCityRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }
        [HttpGet(ApiRoutes.Cities.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetCityByIdQuery(id), cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error.Description);
            }
            return Ok(result.Value);
        }
        [HttpGet(ApiRoutes.Cities.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllCitiesQuery(), cancellationToken);
            return Ok(result.Value);
        }
        [HttpPut(ApiRoutes.Cities.Update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCityRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request,id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return Ok(result.Value);
        }
        [HttpDelete(ApiRoutes.Cities.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteCityCommand(id), cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error.Description);
            }
            return NoContent();
        }
    }
}