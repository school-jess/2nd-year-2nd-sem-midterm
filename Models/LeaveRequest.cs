namespace Evangelist_CRUD_WEB_APP;

public class LeaveRequest
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser User { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType Type { get; set; } // Sick, Casual, Earned
    public string Reason { get; set; }
    public RequestStatus Status { get; set; } // Pending, Approved, Rejected
    public string? ReviewedBy { get; set; } // Manager's UserId
    public DateTime? ReviewedAt { get; set; }
}
