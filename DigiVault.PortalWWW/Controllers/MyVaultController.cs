using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class MyVaultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}