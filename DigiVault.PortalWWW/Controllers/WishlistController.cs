using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class WishlistController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}