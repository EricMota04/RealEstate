using RealEstateApp.Application.DTOs.Image;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IImageRepository : 
        IBaseRepository<Image>,
        IDeletableRepository<Image>
    {
        Task AddImagesAsync(IEnumerable<Image> images);
        Task<IEnumerable<Image>> GetImagesByPropertyAsync(Guid propertyId);
    }
}
