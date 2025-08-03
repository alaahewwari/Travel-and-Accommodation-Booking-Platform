using TABP.Application.Common;
namespace TABP.Application.Roles.Common
{
    public static class RoleErrors
    {
        public static readonly Error RoleNotFound = new(
            Code: "Role.NotFound",
            Description: "Role with this ID does not exist."
        );
        public static readonly Error RoleAlreadyExists = new(
            Code: "Role.AlreadyExists",
            Description: "Role with this number already exists in this Role class."
        );
        public static readonly Error NotModified = new(
            Code: "Role.NotModified",
            Description: "No changes were made to the Role.");
        public static readonly Error InvalidRoleData = new(
            Code: "Role.InvalidData",
            Description: "The provided Role data is invalid."
        );
        public static readonly Error RoleDeletionFailed = new(
            Code: "Role.DeletionFailed",
            Description: "Failed to delete the role."
        );
        public static readonly Error RoleUpdateFailed = new(
            Code: "Role.UpdateFailed",
            Description: "Failed to update the role."
        );
    }
}