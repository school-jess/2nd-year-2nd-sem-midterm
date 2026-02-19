using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace Evangelist_CRUD_WEB_APP.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class ReportsController : Controller
{
    private readonly DatabaseCtx _db;

    public ReportsController(DatabaseCtx db) => _db = db;

    // Daily attendance report
    public async Task<IActionResult> Daily(DateTime? date)
    {
        var selectedDate = date ?? DateTime.Today;

        var report = await _db.Attendances
            .Include(a => a.User)
            .Where(a => a.Date == selectedDate)
            .Select(a => new AttendanceReportViewModel
            {
                EmployeeName = a.User.FullName,
                EmployeeId = a.User.EmployeeId,
                CheckIn = a.CheckInTime,
                CheckOut = a.CheckOutTime,
                Status = a.Status,
                Department = a.User.Department
            })
            .ToListAsync();

        ViewBag.SelectedDate = selectedDate;
        return View(report);
    }

    // Export to CSV
    public async Task<FileResult> ExportCsv(DateTime? startDate, DateTime? endDate)
    {
        var start = startDate ?? DateTime.Today.AddMonths(-1);
        var end = endDate ?? DateTime.Today;

        var data = await _db.Attendances
            .Include(a => a.User)
            .Where(a => a.Date >= start && a.Date <= end)
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("Date,Employee,CheckIn,CheckOut,Status");

        foreach (var item in data)
        {
            csv.AppendLine($"{item.Date},{item.User.FullName},{item.CheckInTime},{item.CheckOutTime},{item.Status}");
        }

        return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", $"attendance-report-{start:yyyy-MM-dd}.csv");
    }
}