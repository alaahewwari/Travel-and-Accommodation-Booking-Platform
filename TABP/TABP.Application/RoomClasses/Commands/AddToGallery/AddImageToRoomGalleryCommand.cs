using MediatR;
using TABP.Application.Common;
namespace TABP.Application.RoomClasses.Commands.AddToGallery
{
    public record AddImageToRoomGalleryCommand(long RoomClassId, Stream FileStream, string FileName) : IRequest<Result>;
}