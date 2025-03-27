using AutoMapper;
using RealEstateApp.Application.DTOs.Client;
using RealEstateApp.Application.Features.Clients.Commands;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile() 
        {
            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.User.ProfilePictureUrl));

            CreateMap<CreateClientCommand, Client>();
        }
    }
}
