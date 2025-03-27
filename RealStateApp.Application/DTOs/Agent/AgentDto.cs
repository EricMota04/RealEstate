using RealEstate.Shared.Enums.Agent;

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
