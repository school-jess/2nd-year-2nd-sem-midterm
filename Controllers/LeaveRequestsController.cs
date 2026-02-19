using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evangelist_CRUD_WEB_APP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly DatabaseCtx _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public LeaveRequestsController(DatabaseCtx db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // Employee: Create new request
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequest request)
    {
        request.ApplicationUserId = _userManager.GetUserId(User);
        request.Status = RequestStatus.Pending;
        request.RequestedAt = DateTime.UtcNow;

        _db.Add(request);
        await _db.SaveChangesAsync();
        return RedirectToAction("MyRequests");
    }

    // Employee: View own requests
    public async Task<IActionResult> MyRequests()
    {
        var userId = _userManager.GetUserId(User);
        var requests = await _db.LeaveRequests
            .Where(l => l.ApplicationUserId == userId)
            .OrderByDescending(l => l.RequestedAt)
            .ToListAsync();
        return View(requests);
    }

    // Manager: View pending requests
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Pending()
    {
        var requests = await _db.LeaveRequests
            .Include(l => l.User)
            .Where(l => l.Status == RequestStatus.Pending)
            .ToListAsync();
        return View(requests);
    }

    // Manager: Approve/Reject
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id, bool approve)
    {
        var request = await _db.LeaveRequests.FindAsync(id);
        if (request == null) return NotFound();

        request.Status = approve ? RequestStatus.Approved : RequestStatus.Rejected;
        request.ReviewedBy = _userManager.GetUserId(User);
        request.ReviewedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return RedirectToAction("Pending");
    }
}
