using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for role data access operations including CRUD operations and role management.
    /// Provides methods for role creation, retrieval, updates, deletion, and role-based queries for user authorization.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Retrieves all roles in the system for administrative and user management purposes.
        /// Returns complete list of roles available for user assignment and authorization.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of all roles in the system with their complete information.</returns>
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Creates a new role in the repository with the provided role information.
        /// Adds the role to the database and persists the changes immediately.
        /// </summary>
        /// <param name="role">The role entity to create with all required properties populated.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created role entity with generated ID and database-assigned values.</returns>
        Task<Role> CreateRoleAsync(Role role, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a role from the repository by its unique identifier.
        /// Removes the role from the database permanently if it exists.
        /// </summary>
        /// <param name="id">The unique identifier of the role to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the role was successfully deleted; false if no role was found with the specified ID.
        /// </returns>
        Task<bool> DeleteRoleAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a role by its unique identifier for role management and authorization operations.
        /// Performs a read-only query to find the role with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the role to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The role with the specified ID if found; otherwise, null if no role exists with that ID.
        /// </returns>
        Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing role's information in the repository.
        /// Modifies the role data and persists the changes to the database.
        /// </summary>
        /// <param name="role">The role entity with updated information including the ID of the role to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated role entity with current data if the update was successful; otherwise, null if no role was found to update.
        /// </returns>
        Task<Role?> UpdateRoleAsync(Role role, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a role by its name for role lookup and validation operations.
        /// Searches for roles using case-sensitive name matching for authorization purposes.
        /// </summary>
        /// <param name="name">The name of the role to search for.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The role with the specified name if found; otherwise, null if no role exists with that name.
        /// </returns>
        Task<Role?> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
    }
}