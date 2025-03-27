using AutoMapper;
using MediatR;
using RealEstateApp.Application.Features.User.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Validators.User;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.User.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResult<bool>>
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new UpdateUserCommandValidator().Validate(request);

            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(validationResult.ToString());

            var user = await _userRepository.FindByIdAsync(request.Id);

            if (user == null)
                return ServiceResult<bool>.Failure("Usuario no encontrado.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.ProfilePictureUrl = request.ProfilePictureUrl;

            await _userRepository.UpdateAsync(user);
            return ServiceResult<bool>.Success(true);

        }
    }
}
