namespace DigiVault.PortalWWW.Models;

public class CoursesIndexViewModel
{
    public IEnumerable<CourseListDto> Courses { get; set; } = [];
    public IEnumerable<CategoryDto> Categories { get; set; } = [];
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public string? CurrentSearch { get; set; }
    public int? CurrentCategory { get; set; }
    public string? CurrentSortBy { get; set; }
}