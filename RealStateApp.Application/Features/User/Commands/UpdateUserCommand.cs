using MediatR;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.User.Commands
{
    public class UpdateUserCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

        public UpdateUserCommand(Guid id)
        {
            Id = id;
        }

    }
}
