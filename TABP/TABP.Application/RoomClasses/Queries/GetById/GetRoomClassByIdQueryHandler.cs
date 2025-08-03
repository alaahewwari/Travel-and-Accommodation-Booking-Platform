using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClassClasses.Common;
using TABP.Application.RoomClasses.Common;
using TABP.Application.RoomClasses.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.RoomClasses.Queries.GetById
{
    public sealed class GetRoomClassByIdQueryHandler(
        IRoomClassRepository repository)
    : IRequestHandler<GetRoomClassByIdQuery, Result<RoomClassResponse>>
    {
        public async Task<Result<RoomClassResponse>> Handle(GetRoomClassByIdQuery request, CancellationToken cancellationToken)
        {
            var roomClass = await repository.GetRoomClassByIdAsync(request.Id, cancellationToken);
            if (roomClass is null)
            {
                return Result<RoomClassResponse>.Failure(RoomClassErrors.RoomClassNotFound);
            }
            var response= roomClass.ToRoomClassResponse();
            return Result<RoomClassResponse>.Success(response);
        }
    }
}