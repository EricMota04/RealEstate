using MediatR;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Images.Commands
{
    public class CreateImageCommand : IRequest<ServiceResult<bool>>
    {
        public Guid PropertyId { get; set; }
        public List<IFormFile> Files { get; set; }

        public CreateImageCommand(Guid propertyId, List<IFormFile> files)
        {
            PropertyId = propertyId;
            Files = files;
        }
    }
}
