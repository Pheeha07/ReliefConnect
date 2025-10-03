using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReliefConnect.Data.Migrations
{
    public partial class UpdateProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Intentionally left empty:
            // We DO NOT create, drop, or rename the Profiles table here.
            // This migration only exists to keep EF's history consistent.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally left empty (no DROP TABLE Profiles).
        }
    }
}
