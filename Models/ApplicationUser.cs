using Microsoft.AspNetCore.Identity;

namespace Evangelist_CRUD_WEB_APP;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string EmployeeId { get; set; }
    public UserRole Role { get; set; } // Enum: Employee, Manager, Admin
    public int? ShiftId { get; set; }
    public Shift Shift { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
    public ICollection<LeaveRequest> LeaveRequests { get; set; }
}
