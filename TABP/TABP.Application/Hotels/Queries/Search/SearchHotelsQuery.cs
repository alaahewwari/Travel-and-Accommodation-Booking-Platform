using MediatR;
using TABP.Application.Common;
using TABP.Domain.Application.Common;
namespace TABP.Application.Hotels.Queries.Search
{
    public record SearchHotelsQuery(
        string? Filters,
        string? Sorts,
        int? Page,
        int? PageSize,
        DateTime? CheckInDate,
        DateTime? CheckOutDate,
        int Adults = 2,
        int Children = 0,
        int Rooms = 1
    ): IRequest<Result<HotelPaginationResult>>;
}