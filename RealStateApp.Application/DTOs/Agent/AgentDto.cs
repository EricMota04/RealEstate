using RealEstate.Shared.Enums.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.DTOs.Agent
{
    public class AgentDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }
        public AgentStatus Status { get; set; }
        public int PropertyCount { get; set; }
    }

}
