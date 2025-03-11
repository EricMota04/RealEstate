namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IDeletableRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity);
    }
}
