using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IPropertyFavoriteRepository : 
        IBaseRepository<PropertyFavorite>, 
        IDeletableRepository<PropertyFavorite>
    {
    }
}
