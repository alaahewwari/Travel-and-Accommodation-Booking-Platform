using MediatR;
using TABP.Application.Common;
namespace TABP.Application.RoomClasses.Commands.SetThumbnail
{
    public record SetRoomThumbnailCommand(long RoomClassId, Stream FileStream, string FileName) : IRequest<Result>;
}