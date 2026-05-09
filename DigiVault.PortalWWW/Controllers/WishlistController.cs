using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class WishlistController(ApiService api) : Controller
{
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        var wishlistTask = api.GetAuthAsync<List<CourseListDto>>("/api/wishlist");
        var cartTask     = api.GetAuthAsync<List<CourseListDto>>("/api/cart");
        await Task.WhenAll(wishlistTask, cartTask);

        var cart = await cartTask ?? [];
        ViewBag.CartIds = cart.Select(c => c.IdCourse).ToHashSet();

        return View(await wishlistTask ?? []);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int idCourse, string? returnUrl)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.PostAuthAsync($"/api/wishlist/{idCourse}");
        return Redirect(returnUrl ?? "/Wishlist");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int idCourse)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.DeleteAuthAsync($"/api/wishlist/{idCourse}");
        return RedirectToAction(nameof(Index));
    }
}
