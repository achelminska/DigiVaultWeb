using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class CoursesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}