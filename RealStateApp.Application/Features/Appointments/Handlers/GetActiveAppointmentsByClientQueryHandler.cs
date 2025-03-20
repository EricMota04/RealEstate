using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Application.Features.Appointments.Querys;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Appointments.Handlers
{
    public class GetActiveAppointmentsByClientQueryHandler : IRequestHandler<GetActiveAppointmentsByClientQuery, ServiceResult<PagedResult<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<GetActiveAppointmentsByClientQueryHandler> _logger;

        private readonly IMapper _mapper;

        public GetActiveAppointmentsByClientQueryHandler(IAppointmentRepository appointmentRepository, ILogger<GetActiveAppointmentsByClientQueryHandler> logger, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResult<PagedResult<AppointmentDto>>> Handle(GetActiveAppointmentsByClientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                PagedResult<AppointmentDto> appointments = await _appointmentRepository
                                                                    .GetActiveAppointmentsByClientAsync(request.ClientId, request.Page, request.PageSize);

                if (appointments.TotalCount == 0)
                    return ServiceResult<PagedResult<AppointmentDto>>.Failure("No appointments found.");

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
