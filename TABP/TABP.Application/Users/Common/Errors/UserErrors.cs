using TABP.Application.Common;
namespace TABP.Application.Users.Common.Errors
{
    public static class UserErrors
    {
        public static readonly Error InvalidCredentials = new(
            Code: "Login.InvalidCredentials",
            Description: "The username or password is incorrect."
        );
        public static readonly Error UserNotFound = new(
            Code: "Login.UserNotFound",
            Description: "User does not exist."
        );
        public static readonly Error AccountLocked = new(
            Code: "Login.AccountLocked",
            Description: "This account is locked. Contact support."
        );
        public static readonly Error UserAlreadyExist = new(
            Code: "User.AlreadyExists",
            Description: "A user with the provided email already exists. Please use a different email or try logging in."
        );
    }
}