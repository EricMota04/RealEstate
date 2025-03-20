using AutoMapper;
using MediatR;
using RealEstateApp.Application.DTOs;
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
    public class GetPagedClientsQueryHandler : IRequestHandler<GetPagedClientsQuery, ServiceResult<PagedResult<ClientDto>>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetPagedClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<PagedResult<ClientDto>>> Handle(GetPagedClientsQuery request, CancellationToken cancellationToken)
        {
            var pagedClients = await _clientRepository.GetAllAsync(request.Page, request.PageSize);

            return ServiceResult<PagedResult<ClientDto>>.Success(pagedClients);
        }
    }
}
