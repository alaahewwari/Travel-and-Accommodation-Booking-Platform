using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Payment;
namespace Application.IntegrationTests.Common.TestInfrastructure
{
    /// <summary>
    /// Test implementation of payment service that allows controlling payment outcomes in tests.
    /// </summary>
    public class TestPaymentService : IPaymentService
    {
        private PaymentResult? _paymentResult;
        private bool _shouldThrow;
        public void SetPaymentResult(PaymentResult result) => _paymentResult = result;
        public void SetShouldThrow(bool shouldThrow) => _shouldThrow = shouldThrow;
        public Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            if (_shouldThrow) throw new Exception("Payment service failure");
            return Task.FromResult(_paymentResult ?? PaymentResult.Success("pi_test_default"));
        }
        public Task<PaymentResult> ConfirmPaymentAsync(string paymentIntentId)
        {
            if (_shouldThrow) throw new Exception("Payment confirmation failure");
            return Task.FromResult(PaymentResult.Success(paymentIntentId));
        }
    }
}