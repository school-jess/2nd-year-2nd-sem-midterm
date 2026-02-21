using Microsoft.AspNetCore.Mvc;

namespace Evangelist_CRUD_WEB_APP.Controllers;

public class EmployeeController : Controller
{
    // GET: Employee
    public ActionResult Index()
    {
        return View();
    }

}
