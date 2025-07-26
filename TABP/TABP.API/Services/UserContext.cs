using System.Security.Claims;
using TABP.Domain.Interfaces.Services;
namespace TABP.API.Services
{
    /// <summary>
    /// Provides information about the currently authenticated user by accessing the HTTP context.
    /// </summary>
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        /// <inheritdoc/>
        public long UserId
        {
            get
            {
                string? value = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return long.TryParse(value, out var id) ? id : 0;
            }
        }
        /// <inheritdoc/>
        public string? Email =>
            httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        /// <inheritdoc/>
        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}