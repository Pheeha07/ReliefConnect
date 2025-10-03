using System;
using System.ComponentModel.DataAnnotations;

namespace ReliefConnect.Models
{
    public class VolunteerTask
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = "";

        [StringLength(2000)]
        public string? Description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Range(1, 10000)]
        public int Capacity { get; set; } = 5;

        public int? ReliefProjectId { get; set; }   // optional link to project
    }
}
