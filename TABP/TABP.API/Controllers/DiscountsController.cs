using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TABP.API.Common;
using TABP.API.Contracts.Discounts;
using TABP.API.Mapping;
using TABP.Application.Discounts.Commands.Delete;
using TABP.Application.Discounts.Common;
using TABP.Application.Discounts.Queries.GetByRoomClass;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Manages room class discount operations such as creation, retrieval, and deletion.
    /// </summary>
    [ApiController]
    //[OutputCache(Duration = 60)]
    public class DiscountsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Adds a discount to a specific room class.
        /// </summary>
        /// <param name="request">The discount details including room class ID, amount, and duration.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The created discount record, or an error response if the operation fails.</returns>
        [HttpPost(ApiRoutes.Discounts.Create)]
        [ProducesResponseType(typeof(DiscountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DiscountResponse>> Create(
            [FromBody] CreateDiscountRequest request,
            [FromRoute] long roomClassId,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(roomClassId);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Created(string.Empty, result.Value);
        }
        /// <summary>
        /// Retrieves all active discounts for a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to fetch discounts for.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>A list of room class discounts associated with the hotel.</returns>
        [HttpGet(ApiRoutes.Discounts.GetByRoomClass)]
        [ProducesResponseType(typeof(IEnumerable<DiscountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DiscountResponse>>> GetByRoomClass(
            [FromRoute] long roomClassId,
            CancellationToken cancellationToken)
        {
            var query = new GetDiscountByRoomClassQuery(roomClassId);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a room class discount by its ID.
        /// </summary>
        /// <param name="id">The ID of the discount to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if deletion is successful, or a 404 error if not found.</returns>
        [HttpDelete(ApiRoutes.Discounts.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteDiscountCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}