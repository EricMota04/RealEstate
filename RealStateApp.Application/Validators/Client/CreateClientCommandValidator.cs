using FluentValidation;
using RealEstateApp.Application.Features.Clients.Commands;

namespace RealEstateApp.Application.Validators.Client
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .NotNull();
        }
    }
}
