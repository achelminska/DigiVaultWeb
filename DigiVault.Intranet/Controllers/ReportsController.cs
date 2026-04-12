using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class ReportsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}