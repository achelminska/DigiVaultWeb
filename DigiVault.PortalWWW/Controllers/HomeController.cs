using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class HomeController(ApiService api) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            PopularCourses = await api.GetAsync<IEnumerable<CourseListDto>>("/api/courses/popular"),
            NewestCourses = await api.GetAsync<IEnumerable<CourseListDto>>("/api/courses/newest"),
            TopRatedCourses = await api.GetAsync<IEnumerable<CourseListDto>>("/api/courses/top-rated"),
            Categories = await api.GetAsync<IEnumerable<CategoryDto>>("/api/categories"),
        };
        
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}