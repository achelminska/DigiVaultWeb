using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class SettingsController : Controller
{
    private readonly ApiService _api;

    public SettingsController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await _api.GetAsync<AdminSettingsDto>("api/admin/settings");
        return View(settings);
    }

    [HttpPost]
    public async Task<IActionResult> Update(decimal commissionRate)
    {
        await _api.PutAsync("api/admin/settings/commission", new { CommissionRate = commissionRate / 100 });
        return RedirectToAction(nameof(Index));
    }
}
