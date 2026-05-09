using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.ViewComponents;

public class CartCountViewComponent(ApiService api, IHttpContextAccessor ctx) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var token = ctx.HttpContext?.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
            return View(0);

        try
        {
            var items = await api.GetAuthAsync<List<CourseListDto>>("/api/cart");
            return View(items?.Count ?? 0);
        }
        catch
        {
            return View(0);
        }
    }
}
