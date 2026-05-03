using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class CategoriesController : Controller
{
    private readonly ApiService _api;

    public CategoriesController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var url = $"api/admin/categories?Page={page}&PageSize=10";
        if (!string.IsNullOrWhiteSpace(search))
            url += $"&Search={search}";

        var result = await _api.GetAsync<PagedResult<CategoryDto>>(url);
        ViewData["Search"] = search;
        ViewData["Page"] = page;
        ViewData["TotalPages"] = result.TotalPages;
        return View(result.Items);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        await _api.PostAsync("api/admin/categories", new { Name = name });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name)
    {
        await _api.PutAsync($"api/admin/categories/{id}", new { IdCategory = id, Name = name });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAsync($"api/admin/categories/{id}");
        return RedirectToAction(nameof(Index));
    }
}
