using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Bookings.Commands.ConfirmPayment
{
    public class ConfirmPaymentCommandHandler(
        IPaymentService paymentService,
        IPaymentRepository paymentRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<ConfirmPaymentCommand, Result>
    {
        public async Task<Result> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine();
            try
            {
                var existingBooking = await bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);
                if (existingBooking is null)
                {
                    return Result.Failure(BookingErrors.BookingNotFound);
                }
                if (existingBooking.Status != BookingStatus.Pending)
                {
                    return Result.Failure(BookingErrors.BookingNotPending);
                }
                var paymentResult = await paymentService.ConfirmPaymentAsync(request.PaymentIntentId);
                if (!paymentResult.IsSuccess)
                {
                    return Result.Failure(BookingErrors.PaymentConfirmationFailed);
                }
                await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
                {
                    existingBooking.Status = BookingStatus.Confirmed;
                    existingBooking.UpdatedAt = DateTime.UtcNow;
                    await bookingRepository.UpdateBookingAsync(existingBooking, cancellationToken);
                    var payment = await paymentRepository.GetByPaymentIntentIdAsync(request.PaymentIntentId, cancellationToken);
                    if (payment != null)
                    {
                        payment.Status = PaymentStatus.Succeeded;
                        payment.ProcessedAt = DateTime.UtcNow;
                        await paymentRepository.UpdateAsync(payment, cancellationToken);
                    }
                    if (existingBooking.Invoice != null)
                    {
                        existingBooking.Invoice.Status = PaymentStatus.Succeeded;
                        existingBooking.Invoice.IssueDate = DateTime.UtcNow;
                    }
                }, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(BookingErrors.PaymentConfirmationFailed);
            }
        }
    }
}