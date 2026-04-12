using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;

namespace DigiVault.PortalWWW.Controllers;

public class CoursesController(ApiService api) : Controller
{
    // GET
    public async Task<IActionResult> Index(int page = 1, string? search = null, int? idCategory = null, string? sortBy = null)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");
        
        var query = new Dictionary<string, string?>
        {
            ["page"]       = page.ToString(),
            ["pageSize"]   = "12",
            ["search"]     = search,
            ["idCategory"] = idCategory?.ToString(),
            ["sortBy"]     = sortBy
        };
            
        var qs = QueryString.Create(query.Where(x => x.Value != null));
        var result = await api.GetAsync<PagedResult<CourseListDto>>($"/api/courses{qs}");
        var categories = await api.GetAsync<IEnumerable<CategoryDto>>("/api/categories");
        
        return View(new CoursesIndexViewModel
        {
            Courses = result?.Items ?? [],
            Categories = categories ?? [],
            CurrentPage = page,
            TotalPages = result?.TotalPages ?? 1,
            CurrentSearch = search,
            CurrentCategory = idCategory,
            CurrentSortBy = sortBy
        });
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        var course = await api.GetAsync<CourseDetailDto>($"/api/courses/{id}");
        if (course == null) return NotFound();

        var reviews = await api.GetAsync<IEnumerable<ReviewDto>>($"/api/courses/{id}/reviews");
        if (reviews == null) return NotFound();

        return View(new CourseDetailViewModel 
        { 
            Course = course, 
            Reviews = reviews ?? []
        });
    }
    
}