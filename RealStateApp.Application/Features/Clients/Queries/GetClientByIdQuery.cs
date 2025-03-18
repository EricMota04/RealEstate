using MediatR;
using RealEstateApp.Application.DTOs.Client;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Clients.Queries
{
    public class GetClientByIdQuery : IRequest<ServiceResult<ClientDto>>
    {
        public Guid ClientId { get; set; }
        public GetClientByIdQuery(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
