using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IAppointmentRepository :
        IBaseRepository<Appointment>
    {
        Task<bool> CancelAppointmentAsync(Guid appointmentId);
        Task<PagedResult<AppointmentDto>> GetActiveAppointmentsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
        Task<PagedResult<AppointmentDto>> GetAppointmentsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
        Task<bool> CompleteAppointmentAsync(Guid appointmentId);
        Task<PagedResult<AppointmentDto>> GetActiveAppointmentsByClientAsync(Guid clientId, int page = 1, int pageSize = 10);
        Task<PagedResult<AppointmentDto>> GetAppointmentsByClientAsync(Guid clientId, int page = 1, int pageSize = 10);
    }
}
