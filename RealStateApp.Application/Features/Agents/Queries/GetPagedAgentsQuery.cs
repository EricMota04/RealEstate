using MediatR;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Queries
{
    public class GetPagedAgentsQuery : IRequest<ServiceResult<PagedResult<AgentDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetPagedAgentsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
