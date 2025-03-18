using MediatR;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Commands
{
    public class CompleteAppointmentCommand : IRequest<ServiceResult<bool>>
    {
        public Guid AppointmentId { get; set; }
        public CompleteAppointmentCommand(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
