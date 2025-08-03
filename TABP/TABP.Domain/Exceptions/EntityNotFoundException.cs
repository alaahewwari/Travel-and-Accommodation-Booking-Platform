namespace TABP.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityType { get; }
        public object EntityId { get; }
        public EntityNotFoundException(string entityType, object entityId)
            : base($"{entityType} with ID '{entityId}' was not found.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}