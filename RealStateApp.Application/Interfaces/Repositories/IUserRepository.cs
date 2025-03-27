using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IUserRepository : 
        IBaseRepository<User>,
        IUpdatableRepository<User>,
        IReadableRepository<UserDto>
    {
    }
}
