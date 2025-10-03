using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<VolunteerTask> VolunteerTasks { get; set; } = null!;
        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; } = null!;
    }
}
