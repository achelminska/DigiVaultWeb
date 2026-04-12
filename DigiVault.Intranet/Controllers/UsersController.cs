using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Detail(int id)
    {
        return View();
    }
}