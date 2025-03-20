using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Features.Appointments.Querys;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class GetAppointmentsByClientQueryHandler : IRequestHandler<GetAppointmentsByClientQuery, ServiceResult<PagedResult<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<GetAppointmentsByClientQueryHandler> _logger;

        public GetAppointmentsByClientQueryHandler(IAppointmentRepository appointmentRepository, ILogger<GetAppointmentsByClientQueryHandler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }
        public async Task<ServiceResult<PagedResult<AppointmentDto>>> Handle(GetAppointmentsByClientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsByClientAsync(request.ClientId, request.Page, request.PageSize);

                return ServiceResult<PagedResult<AppointmentDto>>.Success(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the appointments");
                return ServiceResult<PagedResult<AppointmentDto>>.Failure("An error occured retreiving the appointments");
            }
        }
    }
}
