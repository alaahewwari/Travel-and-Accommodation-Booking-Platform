using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
namespace TABP.Application.Hotels.Queries.GetAll
{
    public class GetAllHotelsQuery: IRequest<Result<IEnumerable<HotelResponse>>>;
}