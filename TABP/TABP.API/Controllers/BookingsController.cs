using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using TABP.API.Common;
using TABP.API.Contracts.Bookings;
using TABP.API.Mappers;
using TABP.Application.Bookings.Commands.Cancel;
using TABP.Application.Bookings.Commands.ConfirmPayment;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Queries.GetById;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure.Configurations;
namespace TABP.API.Controllers
{
    /// <summary>
    /// Manages booking operations including creation, retrieval, deletion, and invoice generation.
    /// </summary>
    [ApiController]
    [Authorize]
    public class BookingsController(ISender mediator, IConfiguration configuration) : ControllerBase
    {
        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="request">The booking details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The newly created booking.</returns>
        [HttpPost(ApiRoutes.Bookings.Create)]
        [ProducesResponseType(typeof(BookingCreationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingCreationResponse>> Create([FromBody] CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(
                    nameof(Create),
                    new { id = result.Value.Id },
                    result.Value
            );
        }
        /// <summary>
        /// Cancels a booking by updating its status to Cancelled.
        /// This is a soft delete - the booking record remains in the database.
        /// </summary>
        /// <param name="id">The booking ID to cancel.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content on successful cancellation.</returns>
        [HttpPatch(ApiRoutes.Bookings.Cancel)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel([FromRoute] long id, CancellationToken cancellationToken)
        {
            var command = new CancelBookingCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return NoContent();
        }
        /// <summary>
        /// Retrieves the invoice of a booking as a PDF file.
        /// </summary>
        /// <param name="id">The booking ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>PDF file stream if successful.</returns>
        [HttpGet(ApiRoutes.Bookings.GetInvoicePdf)]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvoiceAsPdf([FromRoute] long id, CancellationToken cancellationToken)
        {
            var query = new GetInvoiceAsPdfQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            var pdfBytes = result.Value;
            var fileName = $"Invoice-{id}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
        /// <summary>
        /// Retrieves a booking by ID.
        /// </summary>
        /// <param name="id">The booking ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The booking details.</returns>
        [HttpGet(ApiRoutes.Bookings.GetById)]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingResponse>> GetBookingById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetBookingByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);
            if (result.IsFailure)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
        /// <summary>
        /// Retrieves all bookings for the current user.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of bookings.</returns>
        [HttpGet(ApiRoutes.Bookings.GetAll)]
        [ProducesResponseType(typeof(IEnumerable<BookingResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings([FromQuery] GetBookingsRequest request, CancellationToken cancellationToken)
        {
            var query = request.ToQuery();
            var result = await mediator.Send(query, cancellationToken);
            Response.Headers.Append("X-Pagination", result.Value.PaginationMetadata.Build());
            return Ok(result.Value.Items);
        }
        [HttpPost("confirm-payment")]
        public async Task<ActionResult> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
        {
            var command = new ConfirmPaymentCommand(request.BookingId, request.PaymentIntentId);
            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created();
        }
    }
}