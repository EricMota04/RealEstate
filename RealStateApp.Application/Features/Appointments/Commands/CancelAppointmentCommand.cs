using MediatR;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Commands
{
    public class CancelAppointmentCommand : IRequest<ServiceResult<bool>>
    {
        public Guid AppointmentId { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? AgentId { get; set; }
        public CancelAppointmentCommand(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
