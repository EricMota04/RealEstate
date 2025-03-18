using MediatR;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Clients.Commands
{
    public class CreateClientCommand : IRequest<ServiceResult<Guid>>
    {
        public Guid UserId { get; set; }
        public CreateClientCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
