using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.Features.Appointments.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, ServiceResult<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<CancelAppointmentCommandHandler> _logger;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository, ILogger<CancelAppointmentCommandHandler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.FindByIdAsync(request.AppointmentId);

                if (appointment == null)
                    return ServiceResult<bool>.Failure("Appointment not found.");

                // Check if the requesting user is authorized to cancel
                if ((request.ClientId.HasValue && appointment.ClientId != request.ClientId)
                    && (request.AgentId.HasValue && appointment.AgentId != request.AgentId))
                {
                    return ServiceResult<bool>.Failure("You are not authorized to cancel this appointment.");
                }

                var success = await _appointmentRepository.CancelAppointmentAsync(appointment.Id);

                return success ? ServiceResult<bool>.Success(true) : ServiceResult<bool>.Failure("Failed to cancel appointment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while canceling the appointment.");
                return ServiceResult<bool>.Failure("An error occurred while canceling the appointment.");
            }
        }
    }
}
