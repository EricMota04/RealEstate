using RealEstateApp.Application.DTOs;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : 
        IBaseRepository<Property>,
        IUpdatableRepository<Property>,
        IDeletableRepository<Property>,
        IReadableRepository<Property>
    {
        Task<PagedResult<Property>> GetPropertiesByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
    }
}
