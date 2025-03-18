using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IRequestRepository : 
        IBaseRepository<Request>,
        IDeletableRepository<Request>,
        IUpdatableRepository<Request>
    {
        Task<PagedResult<Request>> GetRequestsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
        Task<bool> AcceptRequestAsync(Guid requestId);
        Task<bool> RejectRequestAsync(Guid requestId);
    }
}
