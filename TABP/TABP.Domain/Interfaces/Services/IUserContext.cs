namespace TABP.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides access to information about the currently authenticated user.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the unique identifier of the current user, typically retrieved from the authentication token or claims.
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// Gets the email address of the current user, if available.
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// Indicates whether the current user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }
    }
}