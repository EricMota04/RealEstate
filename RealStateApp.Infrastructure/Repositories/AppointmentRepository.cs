using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly RealEstateDbContext _context;
        private readonly ILogger<AppointmentRepository> _logger;
        private readonly IMapper _mapper;

        public AppointmentRepository(RealEstateDbContext context, ILogger<AppointmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAsync(Appointment entity)
        {
            try
            {
                await _context.Appointments.AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Appointment with id {entity.Id} has been added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the appointment with id {entity.Id}");
                throw;
            }
        }

        public async Task<bool> CancelAppointmentAsync(Guid appointmentId)
        {
            try
            {
                var appointment = await FindByIdAsync(appointmentId);
                if (appointment == null)
                {
                    _logger.LogWarning($"Appointment with id {appointmentId} not found");
                    return false;
                }

                appointment.Status = AppointmentStatus.Canceled;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Appointment with id {appointmentId} has been canceled");
                return true;

            }
            catch (Exception ex)
            {
                _context.ChangeTracker.Clear();
                _logger.LogError(ex, $"An error occurred while canceling the appointment with id {appointmentId}");
                return false;
                throw;
            }
        }

        public async Task<bool> CompleteAppointmentAsync(Guid appointmentId)
        {
            try
            {
                var appointment = await FindByIdAsync(appointmentId);
                if (appointment == null)
                {
                    _logger.LogWarning($"Appointment with id {appointmentId} not found");
                    return false;
                }

                appointment.Status = AppointmentStatus.Completed;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Appointment with id {appointmentId} has been completed");

                return true;
            }
            catch (Exception ex)
            {
                _context.ChangeTracker.Clear();
                _logger.LogError(ex, $"An error occurred while completing the appointment with id {appointmentId}");
                return false;
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return await _context.Appointments.AnyAsync(predicate);
        }

        public async Task<Appointment?> FindByIdAsync(Guid guid)
        {
            try
            {
                var appointment = await _context.Appointments
                                            .Include(a => a.Client)
                                            .Include(a => a.Agent) 
                                            .FirstOrDefaultAsync(a => a.Id == guid);

                if (appointment != null)
                {
                    _logger.LogInformation($"Appointment with id {guid} found");
                    return appointment;
                }
                _logger.LogError($"Appointment with id {guid} not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while finding the appointment with id {guid}");
                throw;

            }

        }

        public async Task<PagedResult<AppointmentDto>> GetActiveAppointmentsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10)
        {
            try
            {
                var appointments = _context.Appointments
                    .Where(a => a.AgentId == agentId && a.Status == AppointmentStatus.Scheduled)
                    .Include(a => a.Agent)
                    .Include(a => a.Client)
                    .AsQueryable();

                var totalItems = await appointments.CountAsync();

                var result = await  appointments.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var appointmentsDto = _mapper.Map<List<AppointmentDto>>(result);
                _logger.LogInformation($"Active appointments for agent {agentId} were found successfully");
                return new PagedResult<AppointmentDto>(appointmentsDto, totalItems, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting active appointments for agent {agentId}");
                throw;
            }
        }

        public async Task<PagedResult<AppointmentDto>> GetAppointmentsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10)
        {
            try
            {
                var appointments = _context.Appointments
                    .Where(a => a.AgentId == agentId)
                    .Include(a => a.Agent)
                    .Include(a => a.Client)
                    .AsQueryable();

                var totalItems = await appointments.CountAsync();

                var result = await appointments.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var appointmentsDto = _mapper.Map<List<AppointmentDto>>(result);
                _logger.LogInformation($"Active appointments for agent {agentId} were found successfully");
                return new PagedResult<AppointmentDto>(appointmentsDto, totalItems, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting active appointments for agent {agentId}");
                throw;
            }
        }

        public async Task<PagedResult<AppointmentDto>> GetAppointmentsByClientAsync(Guid clientId, int page = 1, int pageSize = 10)
        {
            try
            {
                var appointments = _context.Appointments
                    .Where(a => a.ClientId == clientId)
                    .Include(a => a.Agent)
                    .Include(a => a.Client)
                    .AsQueryable();

                var totalItems = await appointments.CountAsync();

                var result = await appointments.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var appointmentsDto = _mapper.Map<List<AppointmentDto>>(result);
                _logger.LogInformation($"Active appointments for client {clientId} were found successfully");
                return new PagedResult<AppointmentDto>(appointmentsDto, totalItems, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting active appointments for client {clientId}");
                throw;
            }
        }

        public async Task<PagedResult<AppointmentDto>> GetActiveAppointmentsByClientAsync(Guid clientId, int page, int pageSize)
        {
            try
            {
                var appointments = _context.Appointments
                    .Where(a => a.ClientId == clientId && a.Status == AppointmentStatus.Scheduled)
                    .Include(a => a.Agent)
                    .Include(a => a.Client)
                    .AsQueryable();

                var totalItems = await appointments.CountAsync();

                var result = await appointments.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var appointmentsDto = _mapper.Map<List<AppointmentDto>>(result);
                _logger.LogInformation($"Active appointments for client {clientId} were found successfully");
                return new PagedResult<AppointmentDto>(appointmentsDto, totalItems, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting active appointments for client {clientId}");
                throw;
            }
        }
    }
}
