using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.Hotels;
using TABP.API.Mapping;
using TABP.Application.Hotels.Commands.Delete;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Queries.GetAll;
using TABP.Application.Hotels.Queries.GetById;
using TABP.Application.Hotels.Queries.GetFeaturedDeals;
namespace TABP.API.Controllers
{
    [ApiController]
    [OutputCache(Duration = 60)]
    public class HotelsController(ISender mediator, HotelMapper mapper) : ControllerBase
    {
        [HttpPost(ApiRoutes.Hotels.Create)]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error.Description);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }
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
                return NotFound(result.Error.Description);
            return Ok(result.Value);
        }
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
        [HttpPut(ApiRoutes.Hotels.Update)]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelResponse>> Update([FromRoute] int id, [FromBody] UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.ToCommand(request, id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error.Description);
            return Ok(result.Value);
        }
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
                return NotFound(result.Error.Description);
            return NoContent();
        }
        [HttpGet(ApiRoutes.Hotels.GetFeaturedDeals)]
        [ProducesResponseType(typeof(HotelFeaturedDealsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelFeaturedDealsResponse>> GetFeaturedDeals(
            [FromQuery] HotelFeaturedDealsRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = new GetHotelFeaturedDealsQuery(request.Count);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error.Description);
            return Ok(result.Value);
        }
    }
}