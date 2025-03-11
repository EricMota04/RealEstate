using System;
using System.ComponentModel.DataAnnotations;
using RealEstate.Shared.Enums;

namespace RealEstateApp.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; } // Client, Agent, Developer, Admin

        [StringLength(500)]
        public string ProfilePictureUrl { get; set; }

        // Relaciones con los perfiles de usuario (Client o Agent)
        public virtual Client? ClientProfile { get; set; }
        public virtual Agent? AgentProfile { get; set; }
    }
}
