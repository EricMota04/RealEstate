using MediatR;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Appointments.Querys
{
    public class GetAppointmentDetailsQuery : IRequest<ServiceResult<AppointmentDetailsDto>>
    {
        public Guid AppointmentId { get; set; }
        public GetAppointmentDetailsQuery(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
