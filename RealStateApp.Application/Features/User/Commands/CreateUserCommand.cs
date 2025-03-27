using MediatR;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<ServiceResult<Guid>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }   
        public string Role { get; set; }
    }
}
