namespace TABP.Domain.Exceptions
{
    public class InvalidUserClaimException : Exception
    {
        public static readonly string MissingUserFields = "User must have a valid username and email.";
        public InvalidUserClaimException(string message) : base(message) { }
    }
}