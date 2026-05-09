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

    [HttpGet]
    public async Task<IActionResult> CourseForm()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        var categories = await api.GetAsync<List<CategoryDto>>("/api/categories");
        return View(categories ?? []);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(string title, string description, decimal price, string? imageUrl, int idCategory)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PostAuthBodyAsync("/api/seller/courses", new
        {
            Title       = title,
            Description = description,
            Price       = price,
            ImageUrl    = imageUrl,
            IdCategory  = idCategory
        });
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditCourse(int id)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        var courseTask     = api.GetAsync<CourseDetailDto>($"/api/courses/{id}");
        var categoriesTask = api.GetAsync<List<CategoryDto>>("/api/categories");
        await Task.WhenAll(courseTask, categoriesTask);

        var course = await courseTask;
        if (course == null) return NotFound();

        ViewBag.Categories = await categoriesTask ?? [];
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> EditCourse(int id, string title, string description, decimal price, string? imageUrl, int idCategory)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PutAuthAsync($"/api/seller/courses/{id}", new
        {
            Title       = title,
            Description = description,
            Price       = price,
            ImageUrl    = imageUrl,
            IdCategory  = idCategory
        });
        return RedirectToAction(nameof(Index));
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
    public async Task<IActionResult> UpdateEmail(string email, string password)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PatchAuthAsync("/api/profile/email", new { Email = email, Password = password });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword(string password, string newPassword, string newPasswordConfirmation)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login");

        await api.PatchAuthAsync("/api/profile/password", new { Password = password, NewPassword = newPassword, NewPasswordConfirmation = newPasswordConfirmation });
        return RedirectToAction(nameof(Index));
    }
}
