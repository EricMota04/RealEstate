using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IPropertyTypeRepository : 
        IBaseRepository<PropertyType>,
        IReadableRepository<PropertyType>,
        IDeletableRepository<PropertyType>,
        IUpdatableRepository<PropertyType>
    {
    }
}
