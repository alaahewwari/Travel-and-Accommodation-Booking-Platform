using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;

namespace TABP.Application.Cities.Commands.SetThumbnail
{
    public class SetCityThumbnailCommandHandler(
        IImageRepository<CityImage> imageRepository,
        ICityRepository cityRepository,
        ICloudinaryService cloudinaryService,
        IUnitOfWork unitOfWork) : IRequestHandler<SetCityThumbnailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SetCityThumbnailCommand request, CancellationToken cancellationToken)
        {
            var existingCity = await cityRepository.GetCityByIdAsync(request.CityId, cancellationToken);
            if (existingCity is null)
            {
                return Result<string>.Failure(CityErrors.CityNotFound);
            }
            var existingThumbnail = await imageRepository.ExistsAsync(request.CityId, ImageType.Thumbnail, cancellationToken);
            if (existingThumbnail)
            {
                await imageRepository.DeleteAsync(request.CityId,ImageType.Thumbnail, cancellationToken);
            }
            string imageUrl = null!;
            await unitOfWork.ExecuteResilientTransactionAsync(async cancellationToken =>
            {
                imageUrl = await cloudinaryService.UploadImageAsync(request.FileStream, request.FileName, "cities", cancellationToken);
                var cityImage = request.ToCityImageDomain(imageUrl, ImageType.Thumbnail);
                await imageRepository.CreateAsync(cityImage, cancellationToken);
            }, cancellationToken);
            return Result<string>.Success(imageUrl!);
        }
    }
}