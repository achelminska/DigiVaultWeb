using DigiVault.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.ViewComponents;

public class CmsFooterViewComponent(CmsService cms) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var copyright = await cms.GetValueAsync("footer.copyright", "© 2026 — All rights reserved");
        return View(model: copyright);
    }
}
