using FluentValidation;
using TABP.API.Contracts.Rooms;
namespace TABP.API.Validators.Rooms
{
    /// <summary>
    /// Validator for room update requests to ensure valid room identification modifications.
    /// Validates updated room number format and ensures compliance with numbering standards.
    /// </summary>
    public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
    {
        /// <summary>
        /// Initializes validation rules for room updates including room number format and length validation.
        /// </summary>
        public UpdateRoomRequestValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Room number is required.")
                .MaximumLength(10).WithMessage("Room number must not exceed 10 characters.")
                .Matches(@"^[A-Za-z0-9-]+$").WithMessage("Room number can only contain letters, numbers, and hyphens.");
        }
    }
}