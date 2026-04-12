using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;
using System.IdentityModel.Tokens.Jwt;

namespace DigiVault.PortalWWW.Controllers;

public class HomeController(ApiService api) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        var model = new HomeViewModel
        {
            PopularCourses  = await api.GetAsync<IEnumerable<CourseListDto>>("/api/courses/popular"),
            NewestCourses   = await api.GetAsync<IEnumerable<CourseListDto>>("/api/courses/newest"),
            TopRatedCourses = await api.GetAsync<IEnumerable<CourseListDto>>("/api/courses/top-rated"),
            Categories      = await api.GetAsync<IEnumerable<CategoryDto>>("/api/categories"),
        };

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        model.UserFirstName = jwt.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value ?? "Gość";

        return View(model);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}