using System;

namespace ReliefConnect.Models
{
    public class ReliefProject
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
