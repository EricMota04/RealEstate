using RealEstateApp.Application.DTOs.Image;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IImageRepository : 
        IBaseRepository<Image>,
        IUpdatableRepository<Image>,
        IDeletableRepository<Image>
    {
        Task<IEnumerable<ImageDto>> GetImagesByPropertyAsync(Guid propertyId);
    }
}
