
using MediatR;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Querys
{
    public class GetAppointmentsByClientQuery : IRequest<ServiceResult<PagedResult<AppointmentDto>>>
    {
        public Guid ClientId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetAppointmentsByClientQuery(Guid clientId, int page, int pageSize)
        {
            ClientId = clientId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
