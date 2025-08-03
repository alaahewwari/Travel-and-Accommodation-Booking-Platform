using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Images;
using TABP.API.Contracts.Rooms;
using TABP.Application.Rooms.Commands.Create;
using TABP.Application.Rooms.Commands.Update;
using TABP.Application.RoomClasses.Commands.AddToGallery;
using TABP.Application.RoomClasses.Commands.SetThumbnail;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class RoomMapping
    {
        public static partial CreateRoomCommand ToCommand(this CreateRoomRequest request, long hotelId, long roomClassId);
        public static partial UpdateRoomCommand ToCommand(this UpdateRoomRequest request, long id);
        public static partial SetRoomThumbnailCommand ToRoomThumbnailCommand(this SetImageRequest request, long roomClassId, Stream fileStream, string fileName);
        public static partial AddImageToRoomGalleryCommand ToRoomGalleryCommand(this SetImageRequest request, long roomClassId, Stream fileStream, string fileName);
    }
}