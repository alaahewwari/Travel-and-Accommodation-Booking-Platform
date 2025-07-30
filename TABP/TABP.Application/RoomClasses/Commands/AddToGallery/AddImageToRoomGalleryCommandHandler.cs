using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClassClasses.Common;
using TABP.Application.RoomClasses.Mapper;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;

namespace TABP.Application.RoomClasses.Commands.AddToGallery
{
    public class AddImageToRoomGalleryCommandHandler(
        IImageRepository<RoomImage> imageRepository,
        IRoomClassRepository roomClassRepository,
        ICloudinaryService cloudinaryService,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<AddImageToRoomGalleryCommand, Result>
    {
        public async Task<Result> Handle(AddImageToRoomGalleryCommand request, CancellationToken cancellationToken)
        {
            var existingRoom = await roomClassRepository.GetRoomClassByIdAsync(request.RoomClassId, cancellationToken);
            if (existingRoom is null)
            {
                return Result.Failure(RoomClassErrors.RoomClassNotFound);
            }
            var existingThumbnail = await imageRepository.ExistsAsync(request.RoomClassId, ImageType.Thumbnail, cancellationToken);
            if (existingThumbnail)
            {
                await imageRepository.DeleteAsync(request.RoomClassId, ImageType.Thumbnail, cancellationToken);
            }
            string? imageUrl = null;
            await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
            {
                var imageUrl = await cloudinaryService.UploadImageAsync(request.FileStream, request.FileName, "rooms", cancellationToken);
                var roomImage = request.ToRoomImageDomain(imageUrl, ImageType.Gallery);
                await imageRepository.CreateAsync(roomImage, cancellationToken);
            }, cancellationToken);
            return Result<string>.Success(imageUrl!);
        }
    }
}