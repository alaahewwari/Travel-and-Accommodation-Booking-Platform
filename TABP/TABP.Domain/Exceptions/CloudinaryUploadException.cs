namespace TABP.Domain.Exceptions
{
    public class CloudinaryUploadException : Exception
    {
        public CloudinaryUploadException(string message)
            : base(message) { }
        public CloudinaryUploadException(string message, Exception inner)
            : base(message, inner) { }
    }
}