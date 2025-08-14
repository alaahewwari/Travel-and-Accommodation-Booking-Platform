using TABP.Domain.Interfaces.Services;
namespace Application.IntegrationTests.Common.TestInfrastructure
{
    /// <summary>
    /// Test implementation of user context for controlling authentication state in tests.
    /// </summary>
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
}