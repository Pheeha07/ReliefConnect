using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore; // needed for [Precision]

namespace ReliefConnect.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required, Range(0.01, 1000000)]
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [Required, StringLength(20)]
        public string PaymentMethod { get; set; } = "Cash";   // Cash / Card / EFT

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int? ReliefProjectId { get; set; }             // optional link to a project

        public string? UserId { get; set; }                   // Identity user id (donor)

        // Donation items
        public ICollection<DonationItem> Items { get; set; } = new List<DonationItem>();
    }
}
