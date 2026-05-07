using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class ReportsController : Controller
{
    private readonly ApiService _api;

    public ReportsController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(int page = 1, bool? isResolved = null)
    {
        var url = $"api/admin/reports?Page={page}&PageSize=10";
        if (isResolved.HasValue) url += $"&IsResolved={isResolved}";

        var result = await _api.GetAsync<PagedResult<AdminReportDto>>(url);
        ViewData["Page"] = page;
        ViewData["TotalPages"] = result?.TotalPages ?? 1;
        ViewData["IsResolved"] = isResolved;
        return View(result?.Items ?? []);
    }

    [HttpPost]
    public async Task<IActionResult> Resolve(int id)
    {
        await _api.PutAsync($"api/admin/reports/{id}/resolve", new { });
        return RedirectToAction(nameof(Index));
    }
}
