using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;         // needed for ModelBuilder, DeleteBehavior, etc.
using ReliefConnect.Models;

namespace ReliefConnect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<ReliefProject> ReliefProjects { get; set; } = null!;
        public DbSet<IncidentReport> IncidentReports { get; set; } = null!;
        public DbSet<Donation> Donations { get; set; } = null!;
        public DbSet<DonationItem> DonationItems { get; set; } = null!;
        public DbSet<VolunteerTask> VolunteerTasks { get; set; } = null!;
        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // decimal precision
            b.Entity<Donation>()
             .Property(d => d.Amount)
             .HasPrecision(18, 2);

            // donation items relationship
            b.Entity<Donation>()
             .HasMany(d => d.Items)
             .WithOne(i => i.Donation)
             .HasForeignKey(i => i.DonationId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
