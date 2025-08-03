using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TABP.Application.Common;
using TABP.Application.RoomClassClasses.Common;
using TABP.Domain.Interfaces.Repositories;

namespace TABP.Application.RoomClasses.Commands.Delete
{
    public sealed class DeleteRoomClassCommandHandler(IRoomClassRepository repository)
    : IRequestHandler<DeleteRoomClassCommand, Result>
    {
        public async Task<Result> Handle(DeleteRoomClassCommand request, CancellationToken cancellationToken)
        {
            var roomClass = await repository.GetRoomClassByIdAsync(request.Id, cancellationToken);
            if (roomClass is null)
            {
                return Result.Failure(RoomClassErrors.RoomClassNotFound);
            }
            await repository.DeleteRoomClassAsync(roomClass.Id, cancellationToken);
            return Result.Success();
        }
    }
}
