namespace Evangelist_CRUD_WEB_APP;

public class Attendance
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser User { get; set; }

    public DateTime Date { get; set; } // e.g., 2026-02-18 (no time)

    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; } // Enum: Present, Late, Absent, HalfDay

    public string Notes { get; set; } // For admin remarks
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
