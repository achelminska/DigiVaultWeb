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

        var courseTask   = api.GetAsync<CourseDetailDto>($"/api/courses/{id}");
        var reviewsTask  = api.GetAsync<IEnumerable<ReviewDto>>($"/api/courses/{id}/reviews");
        var cartTask     = api.GetAuthAsync<List<CourseListDto>>("/api/cart");
        var wishlistTask = api.GetAuthAsync<List<CourseListDto>>("/api/wishlist");
        var purchasedTask = api.GetAuthAsync<List<CourseListDto>>("/api/courses/purchased");

        await Task.WhenAll(courseTask, reviewsTask, cartTask, wishlistTask, purchasedTask);

        var course = await courseTask;
        if (course == null) return NotFound();

        var cart      = await cartTask ?? [];
        var wishlist  = await wishlistTask ?? [];
        var purchased = await purchasedTask ?? [];

        return View(new CourseDetailViewModel
        {
            Course      = course,
            Reviews     = await reviewsTask ?? [],
            IsInCart    = cart.Any(c => c.IdCourse == id),
            IsInWishlist = wishlist.Any(c => c.IdCourse == id),
            IsPurchased  = purchased.Any(c => c.IdCourse == id),
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(int id, int rating, string? comment)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.PostAuthBodyAsync($"/api/courses/{id}/reviews", new { Rating = rating, Comment = comment });
        return RedirectToAction(nameof(Detail), new { id });
    }
}