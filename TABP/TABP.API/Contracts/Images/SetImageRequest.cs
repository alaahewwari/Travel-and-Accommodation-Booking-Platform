namespace TABP.API.Contracts.Images
{
    /// <summary>
    /// Request contract for uploading and setting an image file.
    /// Contains the image file data for thumbnail or gallery operations.
    /// </summary>
    /// <param name="File">The image file to be uploaded and processed.</param>
    public record SetImageRequest(IFormFile File);
}