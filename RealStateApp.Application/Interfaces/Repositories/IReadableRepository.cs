using RealEstateApp.Application.DTOs;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IReadableRepository<TEntity> where TEntity : class
    {
        Task<PagedResult<TEntity>> GetAllAsync(int page = 1, int pageSize = 10);
    }
}
