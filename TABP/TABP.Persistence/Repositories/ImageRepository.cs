using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Entities.Common;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Sealed Entity Framework implementation of the image repository with type-specific entity ID handling.
    /// Provides concrete implementation supporting CityImage, HotelImage, and RoomImage entities with their respective ID properties.
    /// </summary>
    /// <typeparam name="T">The image entity type that inherits from BaseImage (CityImage, HotelImage, or RoomImage).</typeparam>
    /// <param name="context">The Entity Framework database context for image operations.</param>
    public sealed class ImageRepository<T>(ApplicationDbContext context) : IImageRepository<T> where T : BaseImage
    {
        /// <inheritdoc />
        public async Task CreateAsync(T image, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Add(image);
            await context.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task DeleteAsync(long entityId, ImageType imageType, CancellationToken cancellationToken = default)
        {
            var image = await GetAsync(entityId, imageType, cancellationToken);
            if (image is not null)
            {
                context.Set<T>().Remove(image);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
        /// <inheritdoc />
        public async Task<T?> GetAsync(long entityId, ImageType imageType, CancellationToken cancellationToken = default)
        {
            if (typeof(T) == typeof(CityImage))
            {
                return await context.Set<T>()
                    .FirstOrDefaultAsync(img => ((CityImage)(object)img).CityId == entityId && img.ImageType == imageType, cancellationToken);
            }
            else if (typeof(T) == typeof(HotelImage))
            {
                return await context.Set<T>()
                    .FirstOrDefaultAsync(img => ((HotelImage)(object)img).HotelId == entityId && img.ImageType == imageType, cancellationToken);
            }
            else if (typeof(T) == typeof(RoomImage))
            {
                return await context.Set<T>()
                    .FirstOrDefaultAsync(img => ((RoomImage)(object)img).RoomClassId == entityId && img.ImageType == imageType, cancellationToken);
            }
            else
            {
                throw new ArgumentException($"Unknown image type: {typeof(T).Name}");
            }
        }
        /// <inheritdoc />
        public async Task<bool> ExistsAsync(long entityId, ImageType imageType, CancellationToken cancellationToken = default)
        {
            if (typeof(T) == typeof(CityImage))
            {
                return await context.Set<T>()
                    .AnyAsync(img => ((CityImage)(object)img).CityId == entityId && img.ImageType == imageType, cancellationToken);
            }
            else if (typeof(T) == typeof(HotelImage))
            {
                return await context.Set<T>()
                    .AnyAsync(img => ((HotelImage)(object)img).HotelId == entityId && img.ImageType == imageType, cancellationToken);
            }
            else if (typeof(T) == typeof(RoomImage))
            {
                return await context.Set<T>()
                    .AnyAsync(img => ((RoomImage)(object)img).RoomClassId == entityId && img.ImageType == imageType, cancellationToken);
            }
            else
            {
                throw new ArgumentException($"Unknown image type: {typeof(T).Name}");
            }
        }
    }
}