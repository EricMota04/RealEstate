using MediatR;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Appointments.Querys
{
    public class GetAppointmentQuery : IRequest<ServiceResult<AppointmentDto>>
    {
        public Guid AppointmentId { get; set; }
        public GetAppointmentQuery(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
