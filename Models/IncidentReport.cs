using System;
using System.ComponentModel.DataAnnotations;

namespace ReliefConnect.Models
{
    public class IncidentReport
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = "";

        [Required, StringLength(2000)]
        public string Description { get; set; } = "";

        [Required, StringLength(120)]
        public string Location { get; set; } = "";

        [Required]
        public string Severity { get; set; } = "Low"; // Low/Medium/High

        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        public string? ReporterUserId { get; set; }
    }
}
