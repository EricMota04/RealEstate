using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface ISaleTypeRepository : 
        IBaseRepository<SaleType>,
        IReadableRepository<SaleType>,
        IUpdatableRepository<SaleType>,
        IDeletableRepository<SaleType>
    {
    }
}
