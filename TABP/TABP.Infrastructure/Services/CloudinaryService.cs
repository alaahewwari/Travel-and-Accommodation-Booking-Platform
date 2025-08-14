using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TABP.Domain.Constants.Errors;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Services;
namespace TABP.Infrastructure.Services
{
    public class CloudinaryService(
        Cloudinary cloudinary) : ICloudinaryService
    {
        public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string folderName, CancellationToken cancellationToken)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, fileStream),
                    Folder = folderName,
                    UseFilename = false,
                    UniqueFilename = true,
                    Overwrite = false,
                    Transformation = new Transformation()
                        .Quality("auto")
                        .FetchFormat("auto")
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams, cancellationToken);
                if (uploadResult.Error is not null)
                {
                    throw new CloudinaryUploadException($"{CloudinaryErrorMessages.UploadFailed}: {uploadResult.Error.Message}");
                }
                return uploadResult.SecureUrl.ToString();
            }
            catch (CloudinaryUploadException ex)
            {
                throw;
            }
        }
        public async Task DeleteImageAsync(string publicId, CancellationToken cancellationToken)
        {
            try
            {
                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image
                };
                var deleteResult = await cloudinary.DestroyAsync(deleteParams);
                if (deleteResult.Error != null)
                {
                    throw new CloudinaryDeleteException($"{CloudinaryErrorMessages.DeleteFailed}: {deleteResult.Error.Message}");
                }
                if (deleteResult.Result != "ok")
                {
                    throw new CloudinaryDeleteException($"{CloudinaryErrorMessages.UnexpectedDeleteResult}. Result: {deleteResult.Result}");
                }
            }
            catch (CloudinaryDeleteException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CloudinaryDeleteException(CloudinaryErrorMessages.UnexpectedDeleteError, ex);
            }
        }
    }
}