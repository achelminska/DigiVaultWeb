using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class CartController(ApiService api) : Controller
{
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        var items = await api.GetAuthAsync<List<CourseListDto>>("/api/cart");
        return View(items ?? []);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int idCourse, string? returnUrl)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.PostAuthAsync($"/api/cart/{idCourse}");
        return Redirect(returnUrl ?? "/Cart");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int idCourse)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.DeleteAuthAsync($"/api/cart/{idCourse}");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.PostAuthAsync("/api/orders");
        return RedirectToAction("Index", "MyVault");
    }
}
