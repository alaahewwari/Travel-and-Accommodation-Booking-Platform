using FluentValidation;
using TABP.API.Contracts.Images;
namespace TABP.API.Validators.Images
{
    /// <summary>
    /// Validator for image upload requests to ensure valid file format, size, and content type.
    /// Validates image files for thumbnail and gallery operations with security and performance constraints.
    /// </summary>
    public class SetThumbnailValidator : AbstractValidator<SetImageRequest>
    {
        private static readonly string[] AllowedContentTypes =
        {
            "image/jpeg", "image/jpg", "image/png"
        };
        private const long MaxFileSizeBytes = 5_242_880; // 5MB

        /// <summary>
        /// Initializes validation rules for image uploads including file extension, content type, and size validation.
        /// </summary>
        public SetThumbnailValidator()
        {
            RuleFor(x => x.File.FileName)
                .NotEmpty()
                .WithMessage("File name is required")
                .Must(HaveValidExtension)
                .WithMessage("Invalid file extension. Allowed: .jpg, .jpeg, .png");
            RuleFor(x => x.File.ContentType)
                .NotEmpty()
                .WithMessage("Content type is required")
                .Must(BeValidContentType)
                .WithMessage($"Invalid content type. Allowed: {string.Join(", ", AllowedContentTypes)}");
            RuleFor(x => x.File.Length)
                .GreaterThan(0)
                .WithMessage("File size must be greater than 0")
                .LessThanOrEqualTo(MaxFileSizeBytes)
                .WithMessage($"File size must be less than {MaxFileSizeBytes / 1024 / 1024}MB");
        }
        /// <summary>
        /// Validates that the uploaded file has an allowed image extension.
        /// </summary>
        /// <param name="fileName">The file name to validate.</param>
        /// <returns>True if the file extension is allowed, false otherwise.</returns>
        private static bool HaveValidExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension is ".jpg" or ".jpeg" or ".png";
        }
        /// <summary>
        /// Validates that the uploaded file has an allowed MIME content type.
        /// </summary>
        /// <param name="contentType">The content type to validate.</param>
        /// <returns>True if the content type is allowed, false otherwise.</returns>
        private static bool BeValidContentType(string contentType)
        {
            return AllowedContentTypes.Contains(contentType.ToLowerInvariant());
        }
    }
}