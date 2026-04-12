using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class CoursesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}