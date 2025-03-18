using MediatR;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Appointments.Querys
{
    public class GetAppointmentsByAgentQuery : IRequest<ServiceResult<PagedResult<AppointmentDto>>>
    {
        public Guid AgentId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetAppointmentsByAgentQuery(Guid agentId, int page, int pageSize)
        {
            AgentId = agentId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
