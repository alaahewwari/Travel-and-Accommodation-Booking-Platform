namespace TABP.Application.Users.Common
{
    public record UserResponse(
        long Id,
        string Username,
        string Email,
        string FirstName,
        string LastName,
        DateTime CreatedAt
    );
}