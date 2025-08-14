using FluentValidation;
using TABP.API.Contracts.Amenities;
namespace TABP.API.Validators.Amenities
{
    /// <summary>
    /// Validator for amenity update requests to ensure valid amenity information modifications.
    /// Validates updated amenity names and descriptions for hotel and room class features.
    /// </summary>
    public class UpdateAmenityRequestValidator : AbstractValidator<UpdateAmenityRequest>
    {
        /// <summary>
        /// Initializes validation rules for amenity updates including name and description validation.
        /// </summary>
        public UpdateAmenityRequestValidator()
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