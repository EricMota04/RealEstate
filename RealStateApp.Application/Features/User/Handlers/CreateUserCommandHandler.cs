using AutoMapper;
using MediatR;
using RealEstateApp.Application.Features.User.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Application.Validators.User;

namespace RealEstateApp.Application.Features.User.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceResult<Guid>>
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return ServiceResult<Guid>.Failure(validationResult.ToString());

            if (!Enum.TryParse<UserRole>(request.Role, true, out var parsedRole))
                return ServiceResult<Guid>.Failure("Rol inválido."); 

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Role = parsedRole;

            await _userRepository.AddAsync(user);

            return ServiceResult<Guid>.Success(user.Id);
        }
    }
}
