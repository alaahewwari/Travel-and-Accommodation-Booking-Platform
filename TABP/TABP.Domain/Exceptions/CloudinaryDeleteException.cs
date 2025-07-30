namespace TABP.Domain.Exceptions
{
    public class CloudinaryDeleteException : Exception
    {
        public CloudinaryDeleteException(string message)
            : base(message) { }
        public CloudinaryDeleteException(string message, Exception inner)
            : base(message, inner) { }
    }
}