using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Reviews;
using TABP.API.Mappers;
using TABP.Application.Reviews.Commands.Delete;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing reviews including creation, retrieval, update, deletion,
    /// approval, and rejection operations.
    /// </summary>
    [ApiController]
    public class ReviewsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new review. Accessible by authenticated users.
        /// </summary>
        /// <param name="request">The request body containing review details.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The created review details if successful.</returns>
        /// <response code="201">Review created successfully.</response>
        /// <response code="400">Invalid request body or business validation failed.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost(ApiRoutes.Reviews.Create)]
        [Authorize(Roles = UserRoles.Guest)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewResponse>> Create([FromBody] CreateReviewRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result);
        }
        /// <summary>
        /// Retrieves a review by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the review to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The review details if found.</returns>
        /// <response code="200">Review found and returned.</response>
        /// <response code="404">Review not found.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet(ApiRoutes.Reviews.GetById)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewResponse>> GetById([FromRoute] long id, CancellationToken cancellationToken)
        {
            var query = new GetReviewByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Updates an existing review. Only accessible by administrators or the review author.
        /// </summary>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="request">The updated review data.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The updated review details if successful.</returns>
        /// <response code="200">Review updated successfully.</response>
        /// <response code="400">Invalid request body or business rule violation.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks permission to update this review.</response>
        /// <response code="404">Review not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut(ApiRoutes.Reviews.Update)]
        [Authorize(Roles=UserRoles.Guest)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewResponse>> Update([FromRoute] long id, [FromBody] UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
        /// <summary>
        /// Deletes a review by its ID. Only accessible by administrators or the review author.
        /// </summary>
        /// <param name="id">The ID of the review to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>No content if deletion is successful.</returns>
        /// <response code="204">Review deleted successfully.</response>
        /// <response code="404">Review not found.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User lacks permission to delete this review.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete(ApiRoutes.Reviews.Delete)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteReviewCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        } 
    }
}