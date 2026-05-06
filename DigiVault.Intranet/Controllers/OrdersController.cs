using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class OrdersController : Controller
{
    private readonly ApiService _api;

    public OrdersController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(int page = 1, string? dateFrom = null, string? dateTo = null)
    {
        var url = $"api/admin/orders?Page={page}&PageSize=10";
        if (!string.IsNullOrWhiteSpace(dateFrom)) url += $"&DateFrom={dateFrom}";
        if (!string.IsNullOrWhiteSpace(dateTo)) url += $"&DateTo={dateTo}";

        var result = await _api.GetAsync<PagedResult<AdminOrderDto>>(url);
        ViewData["Page"] = page;
        ViewData["TotalPages"] = result?.TotalPages ?? 1;
        ViewData["DateFrom"] = dateFrom;
        ViewData["DateTo"] = dateTo;
        return View(result?.Items ?? []);
    }

    public async Task<IActionResult> Detail(int id)
    {
        return View();
    }
}
