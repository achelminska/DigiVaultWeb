using DigiVault.Intranet.Models;
using DigiVault.Intranet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.Intranet.Controllers;

public class NotificationsController : Controller
{
    private readonly ApiService _api;

    public NotificationsController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(int page = 1, bool isRead = false)
    {
        var url = $"api/admin/notifications?Page={page}&PageSize=10&IsRead={isRead}";
        var result = await _api.GetAsync<PagedResult<AdminNotificationDto>>(url);
        ViewData["Page"] = page;
        ViewData["TotalPages"] = result?.TotalPages ?? 1;
        ViewData["IsRead"] = isRead;
        return View(result?.Items ?? []);
    }

    [HttpPost]
    public async Task<IActionResult> Send(int idUser, string title, string message)
    {
        await _api.PostAsync("api/admin/notifications", new
        {
            IdUser = idUser,
            Title = title,
            Message = message
        });
        return RedirectToAction(nameof(Index));
    }
}
