using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Domain.Models.Common;
namespace TABP.Application.Hotels.Queries.GetAll
{
    public class GetAllHotelsQuery(
        string? Filters,
        string? Sorts,
        int? Page,
        int? PageSize
        )
        : IRequest<Result<PagedResult<HotelForManagementResponse>>>;
}