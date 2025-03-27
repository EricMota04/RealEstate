using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Client;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly RealEstateDbContext _dbContext;
        private readonly ILogger<ClientRepository> _logger;
        private readonly IMapper _mapper;
        public ClientRepository(RealEstateDbContext context, ILogger<ClientRepository> logger, IMapper mapper)
        {
            _dbContext = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddAsync(Client entity)
        {
            try
            {
                await _dbContext.Clients.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Client {entity.Id} was added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the client {entity.Id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Client, bool>> predicate)
        {
            try
            {
                return await _dbContext.Clients.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if a client exists");
                throw;
            }
        }

        public async Task<Client?> FindByIdAsync(Guid guid)
        {
            try
            {
                var client = await _dbContext.Clients.FindAsync(guid);

                if (client == null)
                {
                    _logger.LogWarning($"Client {guid} was not found");
                    return null;
                }
                _logger.LogInformation($"Client {guid} was found successfully");
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while finding the client {guid}");
                throw;
            }
        }

        public async Task<PagedResult<ClientDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                var query = _dbContext.Clients
                    .Include(c => c.User)
                    .AsQueryable();

                int totalClients = await query.CountAsync();

                var clients = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var clientsDto = _mapper.Map<List<ClientDto>>(clients);

                return new PagedResult<ClientDto>(clientsDto, totalClients, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all clients");
                throw;
            }
        }
    }
}
