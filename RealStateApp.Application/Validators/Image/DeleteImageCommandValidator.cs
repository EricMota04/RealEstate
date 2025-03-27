using FluentValidation;
using RealEstateApp.Application.Features.Images.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Validators.Image
{
    public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(p => p.ImageId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The imageId cannot be null or empty.");
        }
    }
}
