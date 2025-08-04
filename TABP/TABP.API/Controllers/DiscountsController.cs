using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Discounts;
using TABP.API.Mappers;
using TABP.Application.Discounts.Commands.Delete;
using TABP.Application.Discounts.Common;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing discounts on room classes,
    /// including creating new discounts and deleting existing ones.
    /// Only users with admin privileges can access these endpoints.
    /// </summary>
    [ApiController]
    // [OutputCache(Duration = 60)] // Optional caching for GET endpoints
    public class DiscountsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Adds a discount to a specific room class. Only accessible by administrators.
        /// </summary>
        /// <param name="request">The discount details including type, value, and duration.</param>
        /// <param name="roomClassId">The ID of the room class to which the discount will be applied.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>The created discount object.</returns>
        /// <response code="201">Discount created successfully.</response>
        /// <response code="400">Invalid request data or business validation failed.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User does not have admin privileges.</response>
        /// <response code="500">Unexpected server error occurred.</response>
        [HttpPost(ApiRoutes.Discounts.Create)]
        [Authorize(Roles = UserRoles.Admin)]
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
        /// Deletes a discount by its unique ID. Only accessible by administrators.
        /// </summary>
        /// <param name="id">The ID of the discount to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>No content if deletion is successful.</returns>
        /// <response code="204">Discount deleted successfully.</response>
        /// <response code="404">Discount not found with the specified ID.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User does not have admin privileges.</response>
        /// <response code="500">Unexpected server error occurred.</response>
        [HttpDelete(ApiRoutes.Discounts.Delete)]
        [Authorize(Roles = UserRoles.Admin)]
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