using TABP.Application.Common;
namespace TABP.Application.Rooms.Common
{
    public static class RoomErrors
    {
        public static readonly Error RoomNotFound = new(
            Code: "Room.NotFound",
            Description: "Room with this ID does not exist."
        );
        public static readonly Error RoomAlreadyExists = new(
            Code: "Room.AlreadyExists",
            Description: "Room with this number already exists in this room class."
        );
        public static readonly Error NotModified = new(
            Code: "Room.NotModified",
            Description: "No changes were made to the Room.");
        public static readonly Error InvalidRoomData = new(
            Code: "Room.InvalidData",
            Description: "The provided Room data is invalid."
        );
        public static readonly Error RoomDoesNotBelongToHotel = new(
            Code: "Room.DoesNotBelongToHotel",
            Description: "The specified room does not belong to the provided hotel."
        );
        public static readonly Error RoomsNotFound = new(
            Code: "Rooms.NotFound",
            Description: "One or more rooms with the provided IDs do not exist."
        );
    }
}