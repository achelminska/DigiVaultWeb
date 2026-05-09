using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class MyVaultController(ApiService api) : Controller
{
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        var courses = await api.GetAuthAsync<List<CourseListDto>>("/api/courses/purchased");
        return View(courses ?? []);
    }
}
