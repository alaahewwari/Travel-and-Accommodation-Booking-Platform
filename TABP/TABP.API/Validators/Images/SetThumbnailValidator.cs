using FluentValidation;
using TABP.API.Contracts.Images;
namespace TABP.API.Validators.Images
{
    public class SetThumbnailValidator : AbstractValidator<SetImageRequest>
    {
        private static readonly string[] AllowedContentTypes =
        {
            "image/jpeg", "image/jpg", "image/png"
        };
        private const long MaxFileSizeBytes = 5_242_880; // 5MB
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
        private static bool HaveValidExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension is ".jpg" or ".jpeg" or ".png";
        }
        private static bool BeValidContentType(string contentType)
        {
            return AllowedContentTypes.Contains(contentType.ToLowerInvariant());
        }
    }
}
