using TABP.Domain.Models.Common;
using TABP.Domain.Models.Hotel;
namespace TABP.Domain.Application.Common
{
    public record HotelPaginationResult(
        PaginationMetadata PaginationMetadata,
        IEnumerable<HotelSearchResultResponse> Items
        );
}