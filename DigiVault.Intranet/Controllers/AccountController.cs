using Microsoft.AspNetCore.Mvc;
using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;

namespace DigiVault.Intranet.Controllers;

public class AccountController(ApiService api) : Controller
{
    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var response = await api.PostWithResponseAsync<LoginResponse>("api/auth/login", new
        {
            login = model.Login,
            password = model.Password
        });

        if (response?.Token is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login or password.");
            return View(model);
        }

        HttpContext.Session.SetString("Token", response.Token);
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
}
