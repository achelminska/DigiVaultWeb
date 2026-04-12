using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class SettingsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}