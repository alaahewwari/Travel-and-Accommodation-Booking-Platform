using Application.IntegrationTests.Common.TestInfrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Services;
using TABP.Persistence.Context;
using Testcontainers.MsSql;
namespace Application.IntegrationTests.Common
{
    /// <summary>
    /// A custom <see cref="WebApplicationFactory{Program}"/> for integration tests.
    /// It spins up a SQL Server container using Testcontainers, configures a dedicated test database,
    /// replaces key services with in-memory fakes, and exposes methods to initialize and clean up the test environment.
    /// </summary>
    /// <remarks>
    /// <para>
    /// • <see cref="InitializeAsync"/> creates a SQL Server testcontainer and ensures the test database schema is created.
    /// </para>
    /// <para>
    /// • <see cref="ConfigureWebHost"/> swaps out real implementations of <see cref="IEmailService"/>,
    ///   <see cref="IPdfService"/>, and <see cref="IUserContext"/> for test doubles,
    ///   and reconfigures <see cref="ApplicationDbContext"/> to point at the containerized database.
    /// </para>
    /// <para>
    /// • <see cref="DisposeAsync"/> stops and removes the testcontainer after all tests complete.
    /// </para>
    /// </remarks>
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Test123!")
            .WithCleanUp(true)
            .Build();
        private string _connectionString = string.Empty;
        /// <summary>
        /// Creates the SQL Server testcontainer and initializes the test database schema (including any model seeds) before tests execute.
        /// </summary>
        public async Task InitializeAsync()
        {
            // Start the container
            await _msSqlContainer.StartAsync();

            // Get the connection string
            _connectionString = _msSqlContainer.GetConnectionString();

            // Create the database schema
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();
        }
        /// <summary>
        /// Configures the test host's DI container to use the containerized database and replace
        /// external services with in-memory test doubles.
        /// </summary>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace email, PDF, user-context, and payment services with test implementations
                services.AddScoped<IEmailService, TestEmailService>();
                services.AddScoped<IPdfService, TestPdfService>();
                services.AddScoped<IUserContext, TestUserContext>();
                services.AddScoped<IPaymentService, TestPaymentService>();

                // Override the ApplicationDbContext to point at the containerized database
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
        /// Stops and removes the SQL Server testcontainer after all tests have run, cleaning up resources.
        /// </summary>
        public async Task DisposeAsync()
        {
            await _msSqlContainer.DisposeAsync();
        }
    }
}