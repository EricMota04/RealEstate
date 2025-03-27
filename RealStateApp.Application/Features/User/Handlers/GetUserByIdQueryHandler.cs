using AutoMapper;
using MediatR;
using RealEstateApp.Application.DTOs.User;
using RealEstateApp.Application.Features.User.Queries;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.User.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ServiceResult<UserDto>>
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId);
            if (user == null)
                return ServiceResult<UserDto>.Failure("User not found.");

            var userDto = _mapper.Map<UserDto>(user);
            return ServiceResult<UserDto>.Success(userDto);
        }

    }
}
