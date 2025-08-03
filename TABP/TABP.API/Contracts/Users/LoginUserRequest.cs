namespace TABP.API.Contracts.Users
{
    /// <summary>
    /// Request contract for user authentication and login.
    /// Contains credentials required for user session creation.
    /// </summary>
    /// <param name="Username">The username for authentication.</param>
    /// <param name="Password">The user's password for verification.</param>
    public record LoginUserRequest(string Username, string Password);
}
