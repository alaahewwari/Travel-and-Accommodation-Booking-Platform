using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;

namespace TABP.Application.RoomClasses.Commands.SetThumbnail
{
    public class SetRoomThumbnailCommandHandler(
        IImageRepository<RoomImage> imageRepository,
        IRoomClassRepository roomClassRepository,
        ICloudinaryService cloudinaryService,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<SetRoomThumbnailCommand, Result>
    {
        public async Task<Result> Handle(SetRoomThumbnailCommand request, CancellationToken cancellationToken)
        {
            var existingRoom = await roomClassRepository.GetRoomClassByIdAsync(request.RoomClassId, cancellationToken);
            if (existingRoom is null)
            {
                return Result.Failure(RoomErrors.RoomNotFound);
            }
            var existingThumbnail = await imageRepository.ExistsAsync(request.RoomClassId, ImageType.Thumbnail, cancellationToken);
            if (existingThumbnail)
            {
                await imageRepository.DeleteAsync(request.RoomClassId, ImageType.Thumbnail, cancellationToken);
            }
            string? uploadedImageUrl = null;
            string? publicIdToCleanup = null;

            try
            {
                // Step 1: Upload to Cloudinary first
                uploadedImageUrl = await cloudinaryService.UploadImageAsync(
                    request.FileStream,
                    request.FileName,
                    "rooms",
                    cancellationToken);
                publicIdToCleanup = ExtractPublicIdFromUrl(uploadedImageUrl);
                await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
                {
                    // Delete existing thumbnail if exists
                    if (await imageRepository.ExistsAsync(request.RoomClassId, ImageType.Thumbnail, cancellationToken))
                    {
                        await imageRepository.DeleteAsync(request.RoomClassId, ImageType.Thumbnail, cancellationToken);
                    }
                    var roomImage = new RoomImage
                    {
                        RoomClassId = request.RoomClassId,
                        ImageUrl = uploadedImageUrl,
                        ImageType = ImageType.Thumbnail,
                        CreatedAt = DateTime.UtcNow
                    };

                    await imageRepository.CreateAsync(roomImage, cancellationToken);
                }, cancellationToken);
                publicIdToCleanup = null;
                return Result<string>.Success(uploadedImageUrl);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(publicIdToCleanup))
                {
                    await cloudinaryService.DeleteImageAsync(publicIdToCleanup, cancellationToken);
                }
                return Result.Failure(RoomErrors.UploadFailed);
            }
        }
        private static string? ExtractPublicIdFromUrl(string imageUrl)
        {
            try
            {
                // URL format: https://res.cloudinary.com/{cloud_name}/image/upload/v{version}/{public_id}.{format}
                var uri = new Uri(imageUrl);
                var segments = uri.AbsolutePath.Split('/');
                if (segments.Length >= 3)
                {
                    var publicIdWithExtension = segments[^1];
                    var publicId = Path.GetFileNameWithoutExtension(publicIdWithExtension);
                    if (segments.Length > 3)
                    {
                        var folderPath = string.Join("/", segments[3..^1]);
                        return $"{folderPath}/{publicId}";
                    }
                    return publicId;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}