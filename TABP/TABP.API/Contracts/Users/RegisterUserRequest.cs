namespace TABP.API.Contracts.Users
{
    /// <summary>
    /// Request contract for registering a new user account.
    /// Contains all required information for user registration including personal details and role assignment.
    /// </summary>
    /// <param name="FirstName">The user's first name.</param>
    /// <param name="LastName">The user's last name.</param>
    /// <param name="Email">The user's email address, used as unique identifier for login.</param>
    /// <param name="Password">The user's password for account security.</param>
    /// <param name="RoleName">The name of the role to assign to the new user.</param>
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string RoleName
    );
}