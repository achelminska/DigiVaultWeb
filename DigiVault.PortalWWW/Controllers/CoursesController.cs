using Microsoft.AspNetCore.Mvc;
using DigiVault.PortalWWW.Models;
using DigiVault.PortalWWW.Services;
using System.IdentityModel.Tokens.Jwt;

namespace DigiVault.PortalWWW.Controllers;

public class CoursesController(ApiService api) : Controller
{
    // GET
    public async Task<IActionResult> Index(int page = 1, string? search = null, int? idCategory = null, string? sortBy = null)
    {
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
        var isLoggedIn = token != null;

        var course  = await api.GetAsync<CourseDetailDto>($"/api/courses/{id}");
        if (course == null) return NotFound();

        var reviews = (await api.GetAsync<IEnumerable<ReviewDto>>($"/api/courses/{id}/reviews") ?? []).ToList();

        var cart = new List<CourseListDto>();
        var wishlist = new List<CourseListDto>();
        var purchased = new List<CourseListDto>();
        ReviewDto? userReview = null;

        if (isLoggedIn)
        {
            cart      = await api.GetAuthAsync<List<CourseListDto>>("/api/cart") ?? [];
            wishlist  = await api.GetAuthAsync<List<CourseListDto>>("/api/wishlist") ?? [];
            purchased = await api.GetAuthAsync<List<CourseListDto>>("/api/courses/purchased") ?? [];

            var jwt       = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var firstName = jwt.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value ?? "";
            var lastName  = jwt.Claims.FirstOrDefault(c => c.Type == "LastName")?.Value ?? "";
            var fullName  = $"{firstName} {lastName}".Trim();
            userReview = reviews.FirstOrDefault(r => r.AuthorName == fullName);
        }

        return View(new CourseDetailViewModel
        {
            Course            = course,
            Reviews           = reviews,
            IsAuthenticated   = isLoggedIn,
            IsInCart          = cart.Any(c => c.IdCourse == id),
            IsInWishlist      = wishlist.Any(c => c.IdCourse == id),
            IsPurchased       = purchased.Any(c => c.IdCourse == id),
            UserReview        = userReview,
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

    [HttpPost]
    public async Task<IActionResult> EditReview(int id, int rating, string? comment)
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null) return RedirectToAction("Login", "Account");

        await api.DeleteAuthAsync($"/api/courses/{id}/reviews");
        await api.PostAuthBodyAsync($"/api/courses/{id}/reviews", new { Rating = rating, Comment = comment });
        return RedirectToAction(nameof(Detail), new { id });
    }
}