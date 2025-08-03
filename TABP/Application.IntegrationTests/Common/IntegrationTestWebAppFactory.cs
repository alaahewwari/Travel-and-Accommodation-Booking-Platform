using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Email;
using TABP.Domain.Models.Payment;
using TABP.Persistence.Context;
namespace Application.IntegrationTests.Common
{
    /// <summary>
    /// A custom <see cref="WebApplicationFactory{Program}"/> for integration tests.
    /// It configures a dedicated test database, replaces key services with in-memory fakes,
    /// and exposes methods to initialize and clean up the test environment.
    /// </summary>
    /// <remarks>
    /// <para>
    /// • <see cref="InitializeAsync"/> ensures the test database schema is created before any tests run.
    /// </para>
    /// <para>
    /// • <see cref="ConfigureWebHost"/> swaps out real implementations of <see cref="IEmailService"/>,
    ///   <see cref="IPdfService"/>, and <see cref="IUserContext"/> for test doubles,
    ///   and reconfigures <see cref="ApplicationDbContext"/> to point at the test database.
    /// </para>
    /// <para>
    /// • <see cref="DisposeAsync"/> drops the test database after all tests complete,
    ///   ensuring a clean slate for subsequent test runs.
    /// </para>
    /// </remarks>
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private string _connectionString = string.Empty;
        /// <summary>
        /// Creates the test database schema (including any model seeds) before tests execute.
        /// </summary>
        public async Task InitializeAsync()
        {
            _connectionString = $"Server=HP-A\\SQLEXPRESS;Integrated Security=True;Database=TravelBookingDb_test;Encrypt=false;";
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();
        }
        /// <summary>
        /// Configures the test host’s DI container to use the test database and replace
        /// external services with in-memory test doubles.
        /// </summary>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace email, PDF, and user-context services with test implementations
                services.AddScoped<IEmailService, TestEmailService>();
                services.AddScoped<IPdfService, TestPdfService>();
                services.AddScoped<IUserContext, TestUserContext>();
                services.AddScoped<IPaymentService, TestPaymentService>();

                // Override the ApplicationDbContext to point at the test database
                var dbDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (dbDescriptor != null)
                    services.Remove(dbDescriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(_connectionString);
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                });
            });
        }
        async Task IAsyncLifetime.DisposeAsync()
        {
            await DisposeAsync();
        }
        /// <summary>
        /// Drops the test database after all tests have run, cleaning up resources.
        /// </summary>
        public class TestEmailService : IEmailService
        {
            public List<EmailMessage> SentEmails { get; } = new();
            public Task SendEmailAsync(EmailMessage emailMessage)
            {
                SentEmails.Add(emailMessage);
                return Task.CompletedTask;
            }
        }
        public class TestPdfService : IPdfService
        {
            public List<string> GeneratedHtml { get; } = new();
            public List<byte[]> GeneratedPdfs { get; } = new();
            public Task<byte[]> GenerateBookingInvoicePdf(string htmlContent)
            {
                GeneratedHtml.Add(htmlContent);
                var fakePdf = System.Text.Encoding.UTF8
                    .GetBytes($"PDF for invoice at {DateTime.UtcNow}");
                GeneratedPdfs.Add(fakePdf);
                return Task.FromResult(fakePdf);
            }
        }
        public class TestUserContext : IUserContext
        {
            public long UserId { get; private set; }
            public string? Email { get; private set; }
            public bool IsAuthenticated => UserId > 0;

            public void SetUser(long userId, string? email = null)
            {
                UserId = userId;
                Email = email;
            }
            public void Clear()
            {
                UserId = 0;
                Email = null;
            }
        }
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
}