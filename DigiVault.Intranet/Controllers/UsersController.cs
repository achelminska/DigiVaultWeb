using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}