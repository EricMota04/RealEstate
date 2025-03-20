using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.Features.Appointments.Querys;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class GetAppointmentDetailsHandler : IRequestHandler<GetAppointmentDetailsQuery, ServiceResult<AppointmentDetailsDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppointmentDetailsHandler> _logger;

        public GetAppointmentDetailsHandler(IAppointmentRepository appointmentRepository, IMapper mapper, ILogger<GetAppointmentDetailsHandler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResult<AppointmentDetailsDto>> Handle(GetAppointmentDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.FindByIdAsync(request.AppointmentId);

                if (appointment == null)
                    return ServiceResult<AppointmentDetailsDto>.Failure($"Appointment with id {request.AppointmentId} not found.");

                var appointmentDetails = _mapper.Map<AppointmentDetailsDto>(appointment);

                return ServiceResult<AppointmentDetailsDto>.Success(appointmentDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the appointment with id {request.AppointmentId}");
                return ServiceResult<AppointmentDetailsDto>.Failure("An error occurred while getting the appointment details.");
            }
        }
    }
}
