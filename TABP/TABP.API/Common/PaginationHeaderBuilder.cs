using TABP.Domain.Models;
namespace TABP.API.Common
{
    public static class PaginationHeaderBuilder
    {
        public static string Build(this PaginationMetadata metadata)
        {
            var paginationHeader = $"totalCount={metadata.TotalCount}; " +
                            $"page={metadata.CurrentPage}; " +
                            $"pageSize={metadata.PageSize}; " +
                            $"totalPages={metadata.TotalPages}";
            return paginationHeader;
        }
    }
}