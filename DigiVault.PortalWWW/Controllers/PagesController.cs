using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class PagesController(CmsService cms) : Controller
{
    public async Task<IActionResult> About()
    {
        var content = await cms.GetValueAsync("about.description",
            "DigiVault to platforma e-learningowa łącząca studentów z ekspertami.");
        return View("Content", new CmsPageViewModel { Title = "O nas", Content = content });
    }

    public async Task<IActionResult> Terms()
    {
        var content = await cms.GetValueAsync("terms.content",
            "Regulamin korzystania z platformy DigiVault.");
        return View("Content", new CmsPageViewModel { Title = "Regulamin", Content = content });
    }

    public async Task<IActionResult> Privacy()
    {
        var content = await cms.GetValueAsync("privacy.content",
            "Polityka prywatności platformy DigiVault.");
        return View("Content", new CmsPageViewModel { Title = "Polityka prywatności", Content = content });
    }

    public async Task<IActionResult> Contact()
    {
        var content = await cms.GetValueAsync("contact.info",
            "Kontakt: support@digivault.example");
        return View("Content", new CmsPageViewModel { Title = "Kontakt", Content = content });
    }
}
