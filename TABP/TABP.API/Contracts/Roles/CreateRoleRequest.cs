namespace TABP.API.Contracts.Rules
{
    /// <summary>
    /// Request contract for creating a new user role in the system.
    /// Contains essential information required for role creation.
    /// </summary>
    /// <param name="Name">The name of the new role to be created.</param>
    public record CreateRoleRequest
    (
        string Name
    );
}