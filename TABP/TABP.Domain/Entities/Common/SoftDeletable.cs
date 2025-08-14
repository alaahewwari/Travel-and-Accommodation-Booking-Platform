namespace TABP.Domain.Entities.Common
{
    public abstract class SoftDeletable
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedOn { get; set; }
    }
}