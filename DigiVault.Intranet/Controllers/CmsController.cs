using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class CmsController(ApiService api) : Controller
{
    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var url = $"api/admin/cms?Page={page}&PageSize=10";
        if (!string.IsNullOrWhiteSpace(search))
            url += $"&Search={Uri.EscapeDataString(search)}";

        var result = await api.GetAsync<PagedResult<AdminCmsContentDto>>(url);
        ViewData["Search"] = search;
        ViewData["Page"] = page;
        ViewData["TotalPages"] = result.TotalPages;
        return View(result.Items);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await api.GetAsync<AdminCmsContentDto>($"api/admin/cms/{id}");
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string key, string title, string value)
    {
        await api.PostAsync("api/admin/cms", new { Key = key, Title = title, Value = value });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string title, string value)
    {
        await api.PutAsync($"api/admin/cms/{id}", new { Title = title, Value = value });
        return RedirectToAction(nameof(Index));
    }
}
