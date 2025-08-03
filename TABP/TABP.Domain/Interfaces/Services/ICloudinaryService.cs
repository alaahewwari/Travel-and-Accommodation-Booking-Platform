namespace TABP.Domain.Interfaces.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(Stream fileStream, string fileName, string folderName, CancellationToken cancellationToken);
        Task DeleteImageAsync(string publicId, CancellationToken cancellationToken);
    }
}