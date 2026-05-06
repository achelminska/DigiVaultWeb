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
        var order = await _api.GetAsync<AdminOrderDetailDto>($"api/admin/orders/{id}");
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        await _api.DeleteAsync($"api/admin/orders/{id}");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCourse(int idOrder, int idCourse)
    {
        await _api.PutAsync($"api/admin/orders/{idOrder}/courses/{idCourse}", new { });
        return RedirectToAction(nameof(Detail), new { id = idOrder });
    }
}
