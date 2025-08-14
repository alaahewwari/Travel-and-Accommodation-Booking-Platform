using TABP.Application.Common;
namespace TABP.Application.Owners.Common
{
    public static class OwnerErrors
    {
        public static readonly Error OwnerNotFound = new(
            Code: "Owner.NotFound",
            Description: "Owner with this ID does not exist."
        );
        public static readonly Error OwnerAlreadyExists = new(
            Code: "Owner.AlreadyExists",
            Description: "Owner with this name already exists."
        );
        public static readonly Error InvalidOwnerData = new(
            Code: "Owner.InvalidData",
            Description: "The provided Owner data is invalid."
        );
        public static readonly Error NotModified = new(
            Code: "Owner.NotModified",
            Description: "The Owner was not modified."
        );
    }
}