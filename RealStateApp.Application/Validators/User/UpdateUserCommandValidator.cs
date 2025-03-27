using FluentValidation;
using RealEstateApp.Application.Features.User.Commands;

namespace RealEstateApp.Application.Validators.User
{

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name must not exceed 100 characters.")
                .Matches("^[a-zA-ZÀ-ÿ ']+$").WithMessage("First Name must contain only letters and spaces.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name must not exceed 100 characters.")
                .Matches("^[a-zA-ZÀ-ÿ ']+$").WithMessage("Last Name must contain only letters and spaces.");

            RuleFor(u => u.ProfilePictureUrl)
                .MaximumLength(500).WithMessage("The photo URL cannot exceed 500 characters.")
                .When(u => !string.IsNullOrWhiteSpace(u.ProfilePictureUrl));
        }
    }
}
