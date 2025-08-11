using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Common;
using TABP.API.Contracts.Bookings;
using TABP.API.Mappers;
using TABP.Application.Bookings.Commands.Cancel;
using TABP.Application.Bookings.Commands.ConfirmPayment;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Queries.GetById;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing hotel bookings including creation, cancellation, payment confirmation,
    /// invoice generation, and retrieval.
    /// </summary>
    [ApiController]
    [Authorize]
    public class BookingsController(ISender mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new hotel booking for the authenticated guest.
        /// </summary>
        /// <param name="request">The booking request details including room ID, dates, and guest information.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>The created booking details including booking ID and confirmation information.</returns>
        /// <response code="201">Booking created successfully.</response>
        /// <response code="400">Invalid input data or business validation failed.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpPost(ApiRoutes.Bookings.Create)]
        [Authorize(Roles = UserRoles.Guest)]
        [ProducesResponseType(typeof(BookingCreationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BookingCreationResponse>> Create([FromBody] CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(Create), new { id = result.Value.Id }, result.Value);
        }
        /// <summary>
        /// Cancels an existing booking identified by its ID. Only the owner of the booking is allowed to cancel.
        /// </summary>
        /// <param name="id">The ID of the booking to cancel.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>No content if cancellation is successful.</returns>
        /// <response code="204">Booking cancelled successfully.</response>
        /// <response code="404">Booking not found or not owned by the user.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpPatch(ApiRoutes.Bookings.Cancel)]
        [Authorize(Roles = UserRoles.Guest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Cancel([FromRoute] long id, CancellationToken cancellationToken)
        {
            var command = new CancelBookingCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
        /// <summary>
        /// Retrieves the invoice associated with a specific booking in PDF format.
        /// </summary>
        /// <param name="id">The ID of the booking.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>A PDF file representing the booking invoice.</returns>
        /// <response code="200">Invoice retrieved successfully.</response>
        /// <response code="404">Booking not found or invoice not available.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpGet(ApiRoutes.Bookings.GetInvoicePdf)]
        [Authorize]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetInvoiceAsPdf([FromRoute] long id, CancellationToken cancellationToken)
        {
            var query = new GetInvoiceAsPdfQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return File(result.Value, "application/pdf", $"Invoice-{id}.pdf");
        }
        /// <summary>
        /// Retrieves detailed information for a specific booking by ID.
        /// </summary>
        /// <param name="id">The ID of the booking to retrieve.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>The booking details.</returns>
        /// <response code="200">Booking retrieved successfully.</response>
        /// <response code="404">Booking not found or not accessible by the user.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpGet(ApiRoutes.Bookings.GetById)]
        [Authorize]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BookingResponse>> GetBookingById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetBookingByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Retrieves a paginated list of all bookings made by the authenticated guest.
        /// </summary>
        /// <param name="request">Pagination and filtering options for retrieving bookings.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>A list of booking records for the current user.</returns>
        /// <response code="200">Bookings retrieved successfully.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpGet(ApiRoutes.Bookings.GetAll)]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<BookingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings([FromQuery] GetBookingsRequest request, CancellationToken cancellationToken)
        {
            var query = request.ToQuery();
            var result = await mediator.Send(query, cancellationToken);
            Response.Headers.Append("X-Pagination", result.Value.PaginationMetadata.Build());
            return Ok(result.Value.Items);
        }
        /// <summary>
        /// Confirms a pending Stripe payment for a booking. Typically triggered after a redirect from a 3D Secure flow.
        /// </summary>
        /// <param name="request">The payment confirmation request containing the booking ID and payment intent ID.</param>
        /// <returns>Confirmation of successful payment.</returns>
        /// <response code="201">Payment confirmed successfully.</response>
        /// <response code="400">Invalid request data or payment could not be confirmed.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpPost("confirm-payment")]
        [Authorize(Roles = UserRoles.Guest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
        {
            var command = new ConfirmPaymentCommand(request.BookingId, request.PaymentIntentId);
            var result = await mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Created();
        }
    }
}