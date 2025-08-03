using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Hotels.Mapper;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;

namespace TABP.Application.Hotels.Commands.AddToGallery
{
    public class AddImageToHotelGalleryCommandHandler(
        IImageRepository<HotelImage> imageRepository,
        IHotelRepository hotelRepository,
        ICloudinaryService cloudinaryService,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<AddImageToHotelGalleryCommand, Result>
    {
        public async Task<Result> Handle(AddImageToHotelGalleryCommand request, CancellationToken cancellationToken)
        {
            var existingHotel = await hotelRepository.GetHotelByIdAsync(request.HotelId, cancellationToken);
            if (existingHotel is null)
            {
                return Result.Failure(HotelErrors.HotelNotFound);
            }
            var existingThumbnail = await imageRepository.ExistsAsync(request.HotelId, ImageType.Thumbnail, cancellationToken);
            if (existingThumbnail)
            {
                await imageRepository.DeleteAsync(request.HotelId, ImageType.Thumbnail, cancellationToken);
            }
            string? imageUrl = null;
            await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
            {
                var imageUrl = await cloudinaryService.UploadImageAsync(request.FileStream, request.FileName, "hotels", cancellationToken);
                var hotelImage = request.ToHotelImageDomain(imageUrl, ImageType.Gallery);
                await imageRepository.CreateAsync(hotelImage, cancellationToken);
            }, cancellationToken);
            return Result<string>.Success(imageUrl!);
        }
    }
}