using AutoMapper;
using RealEstateApp.Application.DTOs.Appointment;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.Property.Id))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Agent.Id))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent.User.FirstName + " " + src.Agent.User.LastName))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.Id))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.User.FirstName + " " + src.Client.User.LastName));

        }
    }
}
