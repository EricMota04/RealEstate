using MediatR;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Querys
{
    public class GetActiveAppointmentsByAgentQuery : IRequest<ServiceResult<PagedResult<AppointmentDto>>>
    {
        public Guid AgentId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetActiveAppointmentsByAgentQuery(Guid agentId, int page, int pageSize)
        {
            AgentId = agentId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
