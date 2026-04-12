using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class CmsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}