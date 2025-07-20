using TABP.Domain.Models;
namespace TABP.Domain.Application.Common
{
    public record HotelPaginationResult(
        PaginationMetadata PaginationMetadata,
        IEnumerable<HotelSearchResultResponse> Items
        );
}