using System;
using System.ComponentModel.DataAnnotations;

namespace ReliefConnect.Models
{
    public class Profile
    {
        [Key]
        public Guid Id { get; set; }   // Same as IdentityUser.Id

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(50)]
        public string? Role { get; set; }   // Admin / Donor / Volunteer

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
