#nullable disable
using Microsoft.EntityFrameworkCore;
using PATBMS_Web.Models;

namespace PATBMS_Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Each DbSet becomes a database table
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<TriageAssessment> TriageAssessments { get; set; }
        public DbSet<DiagnosisRecord> DiagnosisRecords { get; set; }
        public DbSet<BedAllocation> BedAllocations { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<HandoverRecord> HandoverRecords { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}