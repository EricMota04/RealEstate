using MediatR;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Client;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Clients.Queries
{
    public class GetPagedClientsQuery : IRequest<ServiceResult<PagedResult<ClientDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetPagedClientsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
