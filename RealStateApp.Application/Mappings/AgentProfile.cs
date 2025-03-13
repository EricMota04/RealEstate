using AutoMapper;
using RealEstate.Shared.Enums.Agent;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.Features.Agents.Commands;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Mappings
{
    public class AgentProfile : Profile
    {
        public AgentProfile()
        {
            CreateMap<Agent, AgentDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.User.ProfilePictureUrl))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PropertyCount, opt => opt.MapFrom(src => src.Properties.Count));

            CreateMap<CreateAgentCommand, Agent>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AgentStatus.Active));
        }
    }
}
