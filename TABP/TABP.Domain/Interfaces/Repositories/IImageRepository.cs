using TABP.Domain.Entities.Common;
using TABP.Domain.Enums;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IImageRepository<T> where T : BaseImage
    {
        Task CreateAsync(T image, CancellationToken cancellationToken);
        Task DeleteAsync(long entityId, ImageType imageType, CancellationToken cancellationToken);
        Task<T?> GetAsync(long entityId, ImageType imageType, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(long entityId, ImageType imageType, CancellationToken cancellationToken);
    }
}