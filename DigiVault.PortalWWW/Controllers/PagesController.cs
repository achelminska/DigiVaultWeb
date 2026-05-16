using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigiVault.PortalWWW.Controllers;

public class PagesController(CmsService cms) : Controller
{
    public async Task<IActionResult> About()
    {
        var content = await cms.GetValueAsync("about.description",
            "DigiVault is an e-learning platform connecting students with industry experts.");
        return View("Content", new CmsPageViewModel { Title = "About", Content = content });
    }

    public async Task<IActionResult> Terms()
    {
        var content = await cms.GetValueAsync("terms.content",
            "Terms of service for the DigiVault platform.");
        return View("Content", new CmsPageViewModel { Title = "Terms of Service", Content = content });
    }

    public async Task<IActionResult> Privacy()
    {
        var content = await cms.GetValueAsync("privacy.content",
            "Privacy policy for the DigiVault platform.");
        return View("Content", new CmsPageViewModel { Title = "Privacy Policy", Content = content });
    }

    public async Task<IActionResult> Contact()
    {
        var content = await cms.GetValueAsync("contact.info",
            "Contact our team: support@digivault.example");
        return View("Content", new CmsPageViewModel { Title = "Contact", Content = content });
    }
}
