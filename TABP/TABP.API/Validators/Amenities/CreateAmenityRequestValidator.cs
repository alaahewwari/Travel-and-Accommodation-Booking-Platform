using FluentValidation;
using TABP.API.Contracts.Amenities;
namespace TABP.API.Validators.Amenities
{
    /// <summary>
    /// Validator for amenity creation requests to ensure valid amenity information and descriptions.
    /// Validates amenity names and descriptions for hotel and room class features.
    /// </summary>
    public class CreateAmenityRequestValidator : AbstractValidator<CreateAmenityRequest>
    {
        /// <summary>
        /// Initializes validation rules for amenity creation including name uniqueness and description requirements.
        /// </summary>
        public CreateAmenityRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required.")
                .MaximumLength(100).WithMessage("Amenity name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Amenity description is required.")
                .MaximumLength(500).WithMessage("Amenity description must not exceed 500 characters.");
        }
    }
}