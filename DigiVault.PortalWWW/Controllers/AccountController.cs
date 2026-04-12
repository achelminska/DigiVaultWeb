using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}