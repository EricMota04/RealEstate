using AutoMapper;
using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Application.Features.User.Commands;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
