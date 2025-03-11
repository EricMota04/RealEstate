namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IUpdatableRepository<TEntity> where TEntity : class
    {
        Task UpdateAsync(TEntity entity);
    }
}
