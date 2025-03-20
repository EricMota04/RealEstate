using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Application.DTOs.Client;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IClientRepository : 
        IBaseRepository<Client>,
        IReadableRepository<ClientDto>
    {
        
    }
}
