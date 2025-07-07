using TABP.Application.Common;
namespace TABP.Application.RoomClassClasses.Common
{
    public static class RoomClassErrors
    {
        public static readonly Error RoomClassNotFound = new(
            Code: "RoomClass.NotFound",
            Description: "RoomClass with this ID does not exist."
        );
        public static readonly Error InvalidRoomClassData = new(
            Code: "RoomClass.InvalidData",
            Description: "The provided RoomClass data is invalid."
        );
        public static readonly Error NotModified = new(
            Code: "RoomClass.NotModified",
            Description: "No changes were made to the RoomClass."
        );
        public static readonly Error DeleteFailed = new(
            Code: "RoomClass.DeleteFailed",
            Description: "Failed to delete the RoomClass. It may not exist or is already deleted."
        );
    }
}