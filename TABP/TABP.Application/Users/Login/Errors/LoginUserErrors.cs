using TABP.Application.Common.Errors;
namespace TABP.Application.Users.Login.Errors
{
    public static class LoginUserErrors
    {
        public static readonly Error InvalidCredentials = new(
            Code: "Login.InvalidCredentials",
            Description: "The username or password is incorrect."
        );
        public static readonly Error UserNotFound = new(
            Code: "Login.UserNotFound",
            Description: "User with this username does not exist."
        );
        public static readonly Error AccountLocked = new(
            Code: "Login.AccountLocked",
            Description: "This account is locked. Contact support."
        );
    }
}