using FluentValidation;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Application.Features.Images.Commands;

namespace RealEstateApp.Application.Validators.Image
{
    public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        public CreateImageCommandValidator()
        {
            RuleFor(p => p.PropertyId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The propertyId cannot be null or empty.");

            RuleFor(p => p.Files)
                .NotEmpty()
                .NotNull()
                .Must(files => files != null && files.Count > 0)
                .WithMessage("You must upload at least one image.");

            RuleForEach(p => p.Files)
                .Must(IsValidFile)
                .WithMessage("Invalid file format or size. Only JPG, PNG, and max size 5MB are allowed.");
        }

        private bool IsValidFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var maxFileSize = 5 * 1024 * 1024;

            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension) && file.Length <= maxFileSize;
        }
    }
}
