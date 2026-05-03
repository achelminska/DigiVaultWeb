using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class UsersController : Controller
{
    private readonly ApiService _api;

    public UsersController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _api.GetAsync<List<AdminUserDto>>("api/admin/users");
        return View(users);
    }

    public async Task<IActionResult> Detail(int id)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string login, string email, string password,
        string firstName, string lastName, string role)
    {
        await _api.PostAsync("api/admin/users", new
        {
            Login = login,
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            Role = role == "Worker" ? 1 : 0
        });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> SetActive(int id)
    {
        await _api.PostAsync("api/admin/users/set-as-active", new { IdUser = id });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> SetNotActive(int id)
    {
        await _api.PostAsync("api/admin/users/set-as-not-active", new { IdUser = id });
        return RedirectToAction(nameof(Index));
    }
}
