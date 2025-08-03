using Riok.Mapperly.Abstractions;
using System.Runtime.Serialization;
using TABP.Application.Hotels.Common;
using TABP.Application.RoomClasses.Commands.Create;
using TABP.Application.RoomClasses.Commands.Update;
using TABP.Application.RoomClasses.Common;
using TABP.Domain.Entities;
using TABP.Domain.Models;
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
    }
}