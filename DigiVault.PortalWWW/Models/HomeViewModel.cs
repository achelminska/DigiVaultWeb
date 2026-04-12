namespace DigiVault.PortalWWW.Models;

public class HomeViewModel
{
    public IEnumerable<CourseListDto> PopularCourses { get; set; } = [];
    public IEnumerable<CourseListDto> NewestCourses { get; set; } = [];
    public IEnumerable<CourseListDto> TopRatedCourses { get; set; } = [];
    public IEnumerable<CategoryDto> Categories { get; set; } = [];
    public string UserFirstName { get; set; } = string.Empty;
}