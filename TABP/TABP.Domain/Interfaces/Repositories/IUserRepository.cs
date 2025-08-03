using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for user data access operations including authentication and user management.
    /// Provides methods for user authentication, retrieval, and creation with password security handling.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Authenticates a user by verifying their username and password credentials.
        /// Retrieves the user by username and validates the provided password against the stored hash and salt.
        /// </summary>
        /// <param name="username">The username to authenticate.</param>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The authenticated user if credentials are valid; otherwise, null if user doesn't exist or password is incorrect.
        /// </returns>
        Task<User?> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a user by their username for authentication and lookup operations.
        /// Searches the user repository for a user with the specified username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The user with the specified username if found; otherwise, null if no user exists with that username.
        /// </returns>
        Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a user by their unique identifier for profile operations and user management.
        /// Performs a read-only query to find the user with the specified ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The user with the specified ID if found; otherwise, null if no user exists with that ID.
        /// </returns>
        Task<User?> GetUserByIdAsync(long userId, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a user by their email address for account verification and password reset operations.
        /// Performs a read-only query to find the user with the specified email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The user with the specified email address if found; otherwise, null if no user exists with that email.
        /// </returns>
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        /// <summary>
        /// Creates a new user in the repository with the provided user information.
        /// Adds the user to the database and persists the changes immediately.
        /// </summary>
        /// <param name="user">The user entity to create with all required properties populated.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The created user entity with generated ID and database-assigned values if successful; otherwise, null if creation failed.
        /// </returns>
        Task<User?> CreateAsync(User user, CancellationToken cancellationToken);
    }
}