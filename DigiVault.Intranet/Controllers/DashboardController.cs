using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}