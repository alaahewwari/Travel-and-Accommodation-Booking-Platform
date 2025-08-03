namespace TABP.Domain.Models.Common
{
    public record PaginationMetadata(
       int? TotalCount,
       int? TotalPages,
       int? CurrentPage,
       int? PageSize
   );
}