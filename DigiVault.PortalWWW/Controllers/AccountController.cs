using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class AccountController(ApiService api) : Controller
{
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        var profileTask = api.GetAuthAsync<UserProfileDto>("/api/profile");
        var coursesTask = api.GetAuthAsync<PagedResult<CourseListDto>>("/api/seller/courses?page=1&pageSize=50");
        await Task.WhenAll(profileTask, coursesTask);

        var courses = (await coursesTask)?.Items ?? [];

        return View(new AccountViewModel
        {
            Profile   = await profileTask ?? new(),
            MyCourses = courses.ToList(),
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateName(string firstName, string lastName)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PatchAuthAsync("/api/profile/name", new { FirstName = firstName, LastName = lastName });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmail(string email)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PatchAuthAsync("/api/profile/email", new { Email = email });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword(string currentPassword, string newPassword)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PatchAuthAsync("/api/profile/password", new { CurrentPassword = currentPassword, NewPassword = newPassword });
        return RedirectToAction(nameof(Index));
    }
}
