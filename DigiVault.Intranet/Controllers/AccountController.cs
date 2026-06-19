using Microsoft.AspNetCore.Mvc;
using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using System.Text.Json;

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
            ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");
            return View(model);
        }

        var role = GetRoleFromToken(response.Token);
        if (role != "Worker")
        {
            ModelState.AddModelError(string.Empty, "Brak uprawnień. Tylko administratorzy mogą logować się do Intranetu.");
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

    private static string? GetRoleFromToken(string token)
    {
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3) return null;

            var payload = parts[1];
            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));

            using var doc = JsonDocument.Parse(json);
            var roleKey = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            if (doc.RootElement.TryGetProperty(roleKey, out var roleEl))
                return roleEl.GetString();

            return null;
        }
        catch
        {
            return null;
        }
    }
}
