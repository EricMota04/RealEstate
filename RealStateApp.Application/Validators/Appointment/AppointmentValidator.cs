using FluentValidation;
using RealEstateApp.Application.Features.Appointments.Commands;
using RealEstateApp.Application.Interfaces;
using RealEstateApp.Application.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Validators.Appointment
{
    public class AppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentValidator(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;

            RuleFor(x => x.AppointmentId)
                .NotEmpty().WithMessage("AppointmentId is required.");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be greater than today.")
                .MustAsync(BeAvailableDate).WithMessage("The selected appointment time is already booked.");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client is required.");

            RuleFor(x => x.PropertyId)
                .NotEmpty().WithMessage("Property is required.");

            RuleFor(x => x.AgentId)
                .NotEmpty().WithMessage("Agent is required.");
        }

        private async Task<bool> BeAvailableDate(DateTime appointmentDate, CancellationToken cancellationToken)
        {
            // Redondeamos la hora y minuto, eliminando segundos y milisegundos
            var roundedDateTime = appointmentDate.AddSeconds(-appointmentDate.Second).AddMilliseconds(-appointmentDate.Millisecond);

            // Expresión para verificar si ya existe una cita en la misma fecha y hora
            Expression<Func<RealEstateApp.Domain.Entities.Appointment, bool>> appointmentExists = a =>
                a.AppointmentDate.Year == roundedDateTime.Year &&
                a.AppointmentDate.Month == roundedDateTime.Month &&
                a.AppointmentDate.Day == roundedDateTime.Day &&
                a.AppointmentDate.Hour == roundedDateTime.Hour &&
                a.AppointmentDate.Minute == roundedDateTime.Minute;

            // Llamamos al repositorio con la expresión
            return !await _appointmentRepository.ExistsAsync(appointmentExists);
        }
    }
}
