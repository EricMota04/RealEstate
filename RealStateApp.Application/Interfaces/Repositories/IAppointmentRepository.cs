using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IAppointmentRepository : 
        IBaseRepository<Appointment>,
        IUpdatableRepository<Appointment>
    {
        Task<bool> CancelAppointmentAsync(Guid appointmentId);
        Task<PagedResult<Appointment>> GetAppointmentsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
        Task<PagedResult<Appointment>> GetAppointmentsByClientAsync(Guid clientId, int page = 1, int pageSize = 10);
    }
}
