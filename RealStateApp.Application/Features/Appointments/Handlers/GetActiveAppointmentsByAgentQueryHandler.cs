using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Features.Appointments.Querys;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class GetActiveAppointmentsByAgentQueryHandler : IRequestHandler<GetActiveAppointmentsByAgentQuery, ServiceResult<PagedResult<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<GetActiveAppointmentsByAgentQueryHandler> _logger;

        public GetActiveAppointmentsByAgentQueryHandler(IAppointmentRepository appointmentRepository, ILogger<GetActiveAppointmentsByAgentQueryHandler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<PagedResult<AppointmentDto>>> Handle(GetActiveAppointmentsByAgentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _appointmentRepository.GetActiveAppointmentsByAgentAsync(request.AgentId, request.Page, request.PageSize);

                return ServiceResult<PagedResult<AppointmentDto>>.Success(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the appointments");
                return ServiceResult<PagedResult<AppointmentDto>>.Failure("An error occurred while getting the appointments");
            }
        }
    }
}
