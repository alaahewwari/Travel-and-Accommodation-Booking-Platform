using Riok.Mapperly.Abstractions;
using System.Runtime.Serialization;
using TABP.Application.Hotels.Common;
using TABP.Application.RoomClasses.Commands.AddToGallery;
using TABP.Application.RoomClasses.Commands.Create;
using TABP.Application.RoomClasses.Commands.SetThumbnail;
using TABP.Application.RoomClasses.Commands.Update;
using TABP.Application.RoomClasses.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Hotel;
namespace TABP.Application.RoomClasses.Mapper
{
    [Mapper]
    public static partial class RoomClassMapper
    {
        public static partial RoomClassResponse ToRoomClassResponse(this RoomClass roomClass);
        public static RoomClass ToRoomClassDomain(this CreateRoomClassCommand command, Hotel hotel)
        {
            var RoomClass = ToRoomClassDomainInternal(command);
            RoomClass.CreatedAt = DateTime.UtcNow;
            RoomClass.UpdatedAt = DateTime.UtcNow;
            return RoomClass;
        }
        public static RoomClass ToRoomClassDomain(this UpdateRoomClassCommand command)
        {
            var RoomClass = ToRoomClassDomainInternal(command);
            RoomClass.UpdatedAt = DateTime.UtcNow;
            return RoomClass;
        }
        public static partial FeaturedDealsHotelsResponse ToFeaturedDealsHotelsResponse(this FeaturedealsHotels roomClass);
        private static partial RoomClass ToRoomClassDomainInternal(CreateRoomClassCommand command);
        private static partial RoomClass ToRoomClassDomainInternal(UpdateRoomClassCommand command);
        [MapperIgnoreSource(nameof(SetRoomThumbnailCommand.FileStream))]
        [MapperIgnoreSource(nameof(SetRoomThumbnailCommand.FileName))]
        private static partial RoomImage ToRoomImageDomainInternal(SetRoomThumbnailCommand command, string imageUrl);
        public static RoomImage ToRoomImageDomain(this SetRoomThumbnailCommand command, string imageUrl, ImageType type)
        {
            var RoomImage = ToRoomImageDomainInternal(command, imageUrl);
            RoomImage.CreatedAt = DateTime.UtcNow;
            RoomImage.ImageType = type;
            return RoomImage;
        }
        [MapperIgnoreSource(nameof(AddImageToRoomGalleryCommand.FileStream))]
        [MapperIgnoreSource(nameof(AddImageToRoomGalleryCommand.FileName))]
        private static partial RoomImage ToRoomImageDomainInternal(AddImageToRoomGalleryCommand command, string imageUrl);
        public static RoomImage ToRoomImageDomain(this AddImageToRoomGalleryCommand command, string imageUrl, ImageType type)
        {
            var roomImage = ToRoomImageDomainInternal(command, imageUrl);
            roomImage.CreatedAt = DateTime.UtcNow;
            roomImage.ImageType = type;
            return roomImage;
        }
    }
}