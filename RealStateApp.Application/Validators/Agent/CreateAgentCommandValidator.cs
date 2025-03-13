using FluentValidation;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.Features.Agents.Commands;

namespace RealEstateApp.Application.Validators.Agent
{
    public class CreateAgentCommandValidator : AbstractValidator<CreateAgentCommand>
    {
        public CreateAgentCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
