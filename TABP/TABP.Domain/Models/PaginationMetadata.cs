namespace TABP.Domain.Models
{
    public record PaginationMetadata(
       int? TotalCount,
       int? TotalPages,
       int? CurrentPage,
       int? PageSize
   );
}