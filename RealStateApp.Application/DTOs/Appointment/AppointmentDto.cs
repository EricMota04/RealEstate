using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.DTOs.Appointment
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public string PropertyId { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
