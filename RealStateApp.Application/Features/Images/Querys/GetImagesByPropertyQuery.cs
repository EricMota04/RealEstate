using MediatR;
using RealEstateApp.Application.DTOs.Image;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Images.Querys
{
    public class GetImagesByPropertyQuery : IRequest<ServiceResult<List<ImageDto>>>
    {
        public Guid PropertyId { get; set; }
        public GetImagesByPropertyQuery(Guid propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
