using TABP.Domain.Models;
using TABP.Domain.Models.Hotel;
namespace TABP.Domain.Application.Common
{
    public record HotelPaginationResult(
        PaginationMetadata PaginationMetadata,
        IEnumerable<HotelSearchResultResponse> Items
        );
}