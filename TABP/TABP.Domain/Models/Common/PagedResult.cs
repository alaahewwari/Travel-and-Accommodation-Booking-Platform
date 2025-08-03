namespace TABP.Domain.Models.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; }
    }
}