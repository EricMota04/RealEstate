using MediatR;
using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.User.Queries
{
    public class GetUserByIdQuery : IRequest<ServiceResult<UserDto>>
    {
        public Guid UserId { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            UserId = id;
        }
    }
}
