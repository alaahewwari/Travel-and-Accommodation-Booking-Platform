using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Application.IntegrationTests.Common
{
    /// <summary>
    /// Serves as the base class for all integration tests in the application.
    /// It initializes a dedicated dependency injection scope to prevent state leakage
    /// between tests and provides an <see cref="ISender"/> instance for sending
    /// MediatR commands and queries within that scope.
    /// </summary>
    /// <remarks>
    /// <para>
    /// • This class is instantiated once per test class via <see cref="IClassFixture{TFixture}"/>
    ///   using <see cref="IntegrationTestWebAppFactory"/> to spin up the TestServer and DI container.
    /// </para>
    /// <para>
    /// • After tests complete, <see cref="Dispose"/> should be called to release all
    ///   scoped resources (e.g., DbContext, other scoped services).
    /// </para>
    /// <para>
    /// • Using an isolated scope for each test class ensures full isolation of the
    ///   DI container and its scoped services between tests.
    /// </para>
    /// </remarks>
    public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        protected readonly IServiceScope _scope;
        protected ISender Sender;
        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) 
        {
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        }
        protected virtual void Dispose()
        {
            _scope.Dispose();
        }
    }
}