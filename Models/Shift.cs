namespace Evangelist_CRUD_WEB_APP;

public class Shift
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., "Morning Shift"
    public TimeSpan StartTime { get; set; } // 09:00:00
    public TimeSpan EndTime { get; set; }   // 18:00:00
    public int GracePeriodMinutes { get; set; } // 15
    public ICollection<ApplicationUser> Users { get; set; }
}
