namespace TABP.Domain.Exceptions
{
    public class PostBookingOperationsException : BookingCreationException
    {
        public string Operation { get; }
        public PostBookingOperationsException(string operation, Exception innerException)
            : base($"Failed to execute post-booking operation: {operation}", innerException)
        {
            Operation = operation;
        }
    }
}