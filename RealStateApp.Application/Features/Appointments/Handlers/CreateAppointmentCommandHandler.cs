using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.Features.Appointments.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ServiceResult<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<CreateAppointmentCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, ILogger<CreateAppointmentCommandHandler> logger, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResult<bool>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = _mapper.Map<Appointment>(request);

                if (appointment == null)
                    return ServiceResult<bool>.Failure("Appointment not found.");

                await _appointmentRepository.AddAsync(appointment);

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the appointment");
                return ServiceResult<bool>.Failure("An error occurred while handling the appointment");
            }
        }
    }
}
