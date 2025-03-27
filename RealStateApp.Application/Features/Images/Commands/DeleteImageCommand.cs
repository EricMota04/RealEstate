using MediatR;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Images.Commands
{
    public class DeleteImageCommand : IRequest<ServiceResult<bool>>
    {
        public Guid ImageId { get; set; }
        public DeleteImageCommand(Guid imageId)
        {
            ImageId = imageId;
        }
    }
}
