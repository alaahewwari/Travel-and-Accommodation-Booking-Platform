using TABP.Domain.Entities.Common;
using TABP.Domain.Enums;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository interface for image data access operations supporting different image types and entities.
    /// Provides methods for image creation, deletion, retrieval, and existence checking for any entity that inherits from BaseImage.
    /// </summary>
    /// <typeparam name="T">The image entity type that inherits from BaseImage, such as HotelImage, CityImage, or RoomImage.</typeparam>
    public interface IImageRepository<T> where T : BaseImage
    {
        /// <summary>
        /// Creates a new image record in the repository with the provided image information.
        /// Adds the image to the database and persists the changes immediately.
        /// </summary>
        /// <param name="image">The image entity to create with all required properties populated including entity reference and image type.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A task representing the asynchronous create operation.</returns>
        Task CreateAsync(T image, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes an image record from the repository based on entity identifier and image type.
        /// Removes the specified image from the database permanently if it exists.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity that owns the image.</param>
        /// <param name="imageType">The type of image to delete (thumbnail, gallery, etc.).</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteAsync(long entityId, ImageType imageType, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves an image record by entity identifier and image type for display and management operations.
        /// Performs a query to find the specific image associated with the entity and type.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity that owns the image.</param>
        /// <param name="imageType">The type of image to retrieve (thumbnail, gallery, etc.).</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The image with the specified entity ID and type if found; otherwise, null if no image exists.</returns>
        Task<T?> GetAsync(long entityId, ImageType imageType, CancellationToken cancellationToken);
        /// <summary>
        /// Checks whether an image exists for a specific entity and image type without retrieving the full image data.
        /// Performs an efficient existence check for validation and conditional operations.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity to check for image existence.</param>
        /// <param name="imageType">The type of image to check for existence (thumbnail, gallery, etc.).</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>True if an image exists for the specified entity and type; otherwise, false.</returns>
        Task<bool> ExistsAsync(long entityId, ImageType imageType, CancellationToken cancellationToken);
    }
}