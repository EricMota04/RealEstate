using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Shared.Enums.Agent;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly RealEstateDbContext _dbContext;
        private readonly ILogger<AgentRepository> _logger;
        private readonly IMapper _mapper;

        public AgentRepository(RealEstateDbContext dbContext, ILogger<AgentRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task AddAsync(Agent entity)
        {
            try
            {
                await _dbContext.Agents.AddAsync(entity);

                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Agent {entity.Id} was added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the agent {entity.Id}");
                throw;
            }
        }

        public async Task<bool> ChangeAgentStatus(Guid agentId, AgentStatus status)
        {
            try
            {
                var agent = await FindByIdAsync(agentId);

                if (agent == null)
                {
                    _logger.LogWarning($"Agent {agentId} was not found");
                    return false;
                }

                agent.Status = status;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Agent {agentId} status was changed to {status}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while changing the status of the agent {agentId}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Agent, bool>> predicate)
        {
            return await _dbContext.Agents.AnyAsync(predicate);
        }

        public async Task<Agent?> FindByIdAsync(Guid guid)
        {
            try
            {
                var agent = await _dbContext.Agents.FindAsync(guid);

                if (agent == null)
                {
                    _logger.LogWarning($"Agent {guid} was not found");
                    return null;
                }

                _logger.LogInformation($"Agent {guid} was found successfully");
                return agent;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while finding the agent {guid}");
                throw;
            }
        }

        public async Task<PagedResult<AgentDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                var query = _dbContext.Agents
                            .Include(a => a.User)
                            .Include(a => a.Properties)
                            .AsQueryable();

                int totalAgents = await query.CountAsync();

                var agents = await query
                    .OrderBy(a => a.User.LastName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var agentDtos = _mapper.Map<List<AgentDto>>(agents);

                _logger.LogInformation($"Retrieved {agentDtos.Count} agents");
                return new PagedResult<AgentDto>(agentDtos, totalAgents, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all agents");
                throw;
            }
        }

    }
}
