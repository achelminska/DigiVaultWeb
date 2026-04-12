using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class AccountController(ApiService api) : Controller
{
    // GET
    [HttpGet]
    public IActionResult Login() => View();
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var response = await api.PostAsync<LoginResponse>("/api/auth/login", new
        {
            login = model.Login,
            password = model.Password
        });

        if (response?.Token is null)
        {
            ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");
            return View(model);
        }

        HttpContext.Session.SetString("Token", response.Token);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
    
    public IActionResult Index() => View();
    
    public IActionResult CourseForm() => View();
    
    public IActionResult EditCourse(int id) => View("CourseForm");
}