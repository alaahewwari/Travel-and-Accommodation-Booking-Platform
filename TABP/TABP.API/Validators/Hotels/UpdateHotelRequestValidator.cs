using FluentValidation;
using TABP.API.Contracts.Hotels;
namespace TABP.API.Validators.Hotels
{
    /// <summary>
    /// Validator for hotel update requests to ensure valid hotel information modifications.
    /// Validates updated hotel details, address changes, coordinate updates, and entity references.
    /// </summary>
    public class UpdateHotelRequestValidator : AbstractValidator<UpdateHotelRequest>
    {
        /// <summary>
        /// Initializes validation rules for hotel updates including name, address, coordinate, and reference validation.
        /// </summary>
        public UpdateHotelRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hotel name is required.")
                .MaximumLength(255).WithMessage("Hotel name must not exceed 100 characters.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address must not exceed 200 characters.");
            RuleFor(x => x.LocationLatitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
            RuleFor(x => x.LocationLongitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("City ID must be a positive number.");
            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("Owner ID must be a positive number.");
        }
    }
}