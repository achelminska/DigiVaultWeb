using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class OrdersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}