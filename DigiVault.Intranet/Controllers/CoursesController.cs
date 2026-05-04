using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class CoursesController : Controller
{
    private readonly ApiService _api;

    public CoursesController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null, string? isActive = null)
    {
        var url = $"api/admin/courses?Page={page}&PageSize=10";
        if (!string.IsNullOrWhiteSpace(search))
            url += $"&Search={search}";
        if (!string.IsNullOrWhiteSpace(isActive))
            url += $"&IsActive={isActive}";

        var result = await _api.GetAsync<PagedResult<AdminCourseDto>>(url);
        ViewData["Search"] = search;
        ViewData["Page"] = page;
        ViewData["TotalPages"] = result?.TotalPages ?? 1;
        return View(result?.Items ?? []);
    }

    public async Task<IActionResult> Detail(int id)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAsync($"api/admin/courses/{id}");
        return RedirectToAction(nameof(Index));
    }
}
