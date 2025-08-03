using Microsoft.Extensions.Options;
using Stripe;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Payment;
using TABP.Infrastructure.Configurations;
namespace TABP.Infrastructure.Services
{
    /// <summary>
    /// Stripe implementation of payment processing service for handling credit card transactions.
    /// Provides secure payment processing, 3D Secure authentication, and refund capabilities using Stripe APIs.
    /// </summary>
    public class StripePaymentService : IPaymentService
    {
        private readonly IOptionsMonitor<StripeSettings> _settings;
        private readonly StripeClient _stripeClient;
        private readonly PaymentIntentService _paymentIntentService;
        private readonly RefundService _refundService;
        /// <summary>
        /// Initializes the Stripe payment service with configuration and logging capabilities.
        /// </summary>
        /// <param name="configuration">Application configuration containing Stripe API keys.</param>
        public StripePaymentService(IOptionsMonitor<StripeSettings> configuration)
        {
            _settings = configuration;

            // Debug: Check if API key is loaded
            var secretKey = _settings.CurrentValue.SecretKey;
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("Stripe SecretKey is not configured. Check your appsettings.json");
            }

            // Use StripeClient instead of global configuration
            _stripeClient = new StripeClient(secretKey);
            _paymentIntentService = new PaymentIntentService(_stripeClient);
            _refundService = new RefundService(_stripeClient);
        }
        /// <inheritdoc />
        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = ConvertToSmallestUnit(request.Amount, request.Currency),
                    Currency = request.Currency.ToString(),
                    PaymentMethod = request.PaymentMethodId,
                    ConfirmationMethod = "manual",
                    Confirm = true,
                    PaymentMethodTypes = new List<string> { "card" },
                    PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                    {
                        Card = new PaymentIntentPaymentMethodOptionsCardOptions
                        {
                            RequestThreeDSecure = "automatic"
                        }
                    },
                    Metadata = new Dictionary<string, string>
                    {
                        ["booking_id"] = request.BookingId.ToString(),
                        ["customer_email"] = request.CustomerEmail,
                        ["customer_name"] = request.CustomerName
                    },
                    ReceiptEmail = request.CustomerEmail,
                    Description = $"Hotel booking #{request.BookingId}"
                };
                var paymentIntent = await _paymentIntentService.CreateAsync(options);
                return MapPaymentIntentToResult(paymentIntent);
            }
            catch (StripeException ex)
            {
                return PaymentResult.Failed(ex.Message);
            }
            catch (Exception ex)
            {
                return PaymentResult.Failed("An unexpected error occurred while processing payment");
            }
        }
        /// <inheritdoc />
        public async Task<PaymentResult> ConfirmPaymentAsync(string paymentIntentId)
        {
            try
            {
                var options = new PaymentIntentConfirmOptions();
                var paymentIntent = await _paymentIntentService.ConfirmAsync(paymentIntentId, options);
                return MapPaymentIntentToResult(paymentIntent);
            }
            catch (StripeException ex)
            {
                return PaymentResult.Failed(ex.Message);
            }
            catch (Exception ex)
            {
                return PaymentResult.Failed(ex.Message);
            }
        }
        /// <summary>
        /// Maps Stripe PaymentIntent to our PaymentResult.
        /// </summary>
        private static PaymentResult MapPaymentIntentToResult(PaymentIntent paymentIntent)
        {
            return paymentIntent.Status switch
            {
                "succeeded" => PaymentResult.Success(paymentIntent.Id),
                "requires_action" => PaymentResult.RequiresConfirmation(paymentIntent.Id, paymentIntent.ClientSecret),
                "requires_payment_method" => PaymentResult.Failed("Payment method was declined"),
                "requires_confirmation" => PaymentResult.RequiresConfirmation(paymentIntent.Id, paymentIntent.ClientSecret),
                "processing" => PaymentResult.Pending(paymentIntent.Id, paymentIntent.ClientSecret),
                "canceled" => PaymentResult.Failed("Payment was cancelled"),
                _ => PaymentResult.Failed(paymentIntent.LastPaymentError?.Message ?? "Payment failed")
            };
        }
        /// <summary>
        /// Converts a decimal amount to the smallest currency unit (e.g., cents, pence, yen).
        /// Ensures correct rounding based on currency precision.
        /// </summary>
        private static long ConvertToSmallestUnit(decimal amount, PriceCurrency currency)
        {
            // Determine how many decimal places are allowed
            var decimalPlaces = currency switch
            {
                PriceCurrency.JPY => 0,
                _ => 2 // Default: USD, EUR, GBP, etc.
            };
            var rounded = Math.Round(amount, decimalPlaces, MidpointRounding.AwayFromZero);
            var multiplier = (decimal)Math.Pow(10, decimalPlaces);
            return (long)(rounded * multiplier);
        }
    }
}