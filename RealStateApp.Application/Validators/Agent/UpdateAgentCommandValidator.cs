using FluentValidation;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.Features.Agents.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Validators.Agent
{
    public class UpdateAgentCommandValidator : AbstractValidator<UpdateAgentCommand>
    {
        public UpdateAgentCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid agent status.");
        }
    }
}
