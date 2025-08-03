namespace TABP.Domain.Exceptions
{
    public class BookingOverlapException : BookingCreationException
    {
        public IEnumerable<long> ConflictingRoomIds { get; }
        public BookingOverlapException(IEnumerable<long> conflictingRoomIds)
            : base("One or more rooms are already booked for the selected dates.")
        {
            ConflictingRoomIds = conflictingRoomIds;
        }
    }
}