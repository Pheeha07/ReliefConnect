using System;
using System.ComponentModel.DataAnnotations;

namespace ReliefConnect.Models
{
    public class VolunteerAssignment
    {
        public int Id { get; set; }

        [Required]
        public int VolunteerTaskId { get; set; }       // FK column

        // ✅ Proper navigation type (NOT object)
        public VolunteerTask VolunteerTask { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = "";       // Identity user id

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string Status { get; set; } = "Pending"; // Pending/Approved/Completed
    }
}
