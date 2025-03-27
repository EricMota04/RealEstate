using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealEstateDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;

        public UserRepository(RealEstateDbContext context, ILogger<UserRepository> logger, IMapper mapper)
        {
            _dbContext = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddAsync(User entity)
        {
            try
            {
                await _dbContext.Users.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"User {entity.Id} added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the user {entity.Id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await _dbContext.Users.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if a user exists");
                throw;
            }
        }

        public async Task<User?> FindByIdAsync(Guid guid)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(guid);
                if (user == null)
                {
                    _logger.LogWarning($"User {guid} was not found");
                    return null;
                }
                _logger.LogInformation($"User {guid} was found successfully");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while finding the user {guid}");
                throw;
            }
        }

        public async Task<PagedResult<UserDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {

            try
            {
                var query = _dbContext.Users.AsQueryable();

                var totalUsers = await query.CountAsync();

                var users = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var userDtos = _mapper.Map<List<UserDto>>(users);

                return new PagedResult<UserDto>(userDtos, totalUsers, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users");
                throw;
            }
        }

        public async Task UpdateAsync(User entity)
        {
            try
            {
                var existingUser = await FindByIdAsync(entity.Id);
                if (existingUser == null)
                {
                    _logger.LogWarning($"User {entity.Id} was not found");
                    throw new KeyNotFoundException($"User {entity.Id} was not found");
                }

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.ProfilePictureUrl = entity.ProfilePictureUrl;

                //_dbContext.Entry(existingUser).CurrentValues.SetValues(entity);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"User {entity.Id} was updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the user {entity.Id}");
                throw;
            }
        }
    }
}
