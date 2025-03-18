using MediatR;
using RealEstateApp.Application.Features.Clients.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Validators.Client;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Clients.Handlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ServiceResult<Guid>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository, IUserRepository user)
        {
            _clientRepository = clientRepository;
            _userRepository = user;
        }

        public async Task<ServiceResult<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var clientValidation = new CreateClientCommandValidator().Validate(request);
            
            if (!clientValidation.IsValid)
                return ServiceResult<Guid>.Failure(clientValidation.Errors.Select(e => e.ErrorMessage).ToList());

            // Verificar si el usuario existe.
            var user = await _userRepository.FindByIdAsync(request.UserId);
            if (user == null)
                return ServiceResult<Guid>.Failure("User not found.");

            // Crear peril de Cliente
            var client = new Client
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId
            };

            await _clientRepository.AddAsync(client);
            return ServiceResult<Guid>.Success(client.Id);
        }
    }
}
