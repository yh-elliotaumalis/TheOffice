using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmployeeAbsence> EmployeeAbsences { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeReport> TimeReports { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Invoice>()
                .HasOne(e => e.Project)
                .WithMany(e => e.Invoices)
                .HasForeignKey(i  => i.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
