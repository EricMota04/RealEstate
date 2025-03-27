using FluentValidation;
using RealEstateApp.Application.Features.User.Commands;
using RealEstateApp.Application.Interfaces.Repositories;

namespace RealEstateApp.Application.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name must not exceed 100 characters.")
                .Matches("^[a-zA-ZÀ-ÿ ']+$").WithMessage("First Name must contain only letters and spaces.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name must not exceed 100 characters.")
                .Matches("^[a-zA-ZÀ-ÿ ']+$").WithMessage("Last Name must contain only letters and spaces.");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role is required.")
                .IsEnumName(typeof(UserRole), caseSensitive: false)
                .WithMessage("Invalid role. Must be one of the values ​​defined in UserRole\r\n.");

            RuleFor(u => u.ProfilePictureUrl)
                .MaximumLength(500).WithMessage("The photo URL cannot exceed 500 characters.")
                .When(u => !string.IsNullOrWhiteSpace(u.ProfilePictureUrl));

            // If the role is Agent, the photo is required
            When(u => u.Role.Equals("Agent", StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(u => u.ProfilePictureUrl)
                    .NotEmpty().WithMessage("Agents must upload a profile photo.");
            });

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("You must enter a valid email.")
                .MaximumLength(150).WithMessage("The email cannot exceed 150 characters.");

            RuleFor(u => u.Email)
                .MustAsync(async (email, cancellation) =>
                    !await _userRepository.ExistsAsync(u => u.Email == email))
                .WithMessage("This email is already registered");
        }
    }
}
