using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class CartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}