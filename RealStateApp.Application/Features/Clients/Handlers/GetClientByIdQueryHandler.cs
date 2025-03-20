using AutoMapper;
using MediatR;
using RealEstateApp.Application.DTOs.Client;
using RealEstateApp.Application.Features.Clients.Queries;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Clients.Handlers
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ServiceResult<ClientDto>>
    {
        public readonly IClientRepository _clientRepository;
        public readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.FindByIdAsync(request.ClientId);
            if (client == null)
                return ServiceResult<ClientDto>.Failure("Client not found.");   

            var clientDto = _mapper.Map<ClientDto>(client);
            return ServiceResult<ClientDto>.Success(clientDto);
        }
    }
}
