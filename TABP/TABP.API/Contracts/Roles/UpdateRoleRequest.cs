namespace TABP.API.Contracts.Rules
{
    /// <summary>
    /// Request contract for updating an existing user role.
    /// Contains modifiable properties of a role definition.
    /// </summary>
    /// <param name="Name">The updated name for the role.</param>
    public record UpdateRoleRequest
    (
        string Name
    );
}