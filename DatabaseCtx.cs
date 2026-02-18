using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evangelist_CRUD_WEB_APP;

public class DatabaseCtx : IdentityDbContext<ApplicationUser>
{
    public DatabaseCtx(DbContextOptions<DatabaseCtx> options)
        : base(options) { }

    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Add any custom configurations here
        builder.Entity<Attendance>()
            .HasIndex(a => new { a.ApplicationUserId, a.Date })
            .IsUnique(); // One attendance record per user per day

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Shift)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.ShiftId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
