using MediatR;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Commands
{
    public class CreateAgentCommand : IRequest<ServiceResult<Guid>>
    {
        public Guid UserId { get; set; }

        public CreateAgentCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
