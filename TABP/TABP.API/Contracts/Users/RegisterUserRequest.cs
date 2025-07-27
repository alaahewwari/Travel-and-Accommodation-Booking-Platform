namespace TABP.API.Contracts.Users
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string RoleName
    );
}