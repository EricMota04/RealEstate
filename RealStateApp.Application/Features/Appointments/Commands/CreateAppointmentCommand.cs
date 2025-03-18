using MediatR;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Appointments.Commands
{
    public class CreateAppointmentCommand : IRequest<ServiceResult<bool>>
    {
        public Guid AppointmentId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid AgentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
    }
}
