using MediatR;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.User.Queries
{
    public class GetPagedUsersQuery : IRequest<ServiceResult<PagedResult<UserDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetPagedUsersQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
