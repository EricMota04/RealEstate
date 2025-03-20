using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.Features.Appointments.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class CompleteAppointmentCommandHandler : IRequestHandler<CompleteAppointmentCommand, ServiceResult<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<CompleteAppointmentCommandHandler> _logger;

        public CompleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository, ILogger<CompleteAppointmentCommandHandler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }
        public async Task<ServiceResult<bool>> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var appointment = await _appointmentRepository.FindByIdAsync(request.AppointmentId);

                if(appointment.AgentId != request.AgentId)
                    return ServiceResult<bool>.Failure("You are not authorized to complete this appointment.");

                if (appointment == null)
                    return ServiceResult<bool>.Failure("Appointment not found.");

                var success = await _appointmentRepository.CompleteAppointmentAsync(appointment.Id);

                if (!success)
                    return ServiceResult<bool>.Failure("There was an error completing the appointment");

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while completing the appointment");
                return ServiceResult<bool>.Failure("An error occurred while completing the appointment");
            }
        }
    }
}
