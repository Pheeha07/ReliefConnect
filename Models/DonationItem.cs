using System.ComponentModel.DataAnnotations;

namespace ReliefConnect.Models
{
    public class DonationItem
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Description { get; set; } = "";

        [Range(1, 100000)]
        public int Quantity { get; set; } = 1;

        public int DonationId { get; set; }
        public Donation Donation { get; set; } = null!;
    }
}
