namespace DigiVault.Intranet.Models;

public class AdminCourseDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SalesCount { get; set; }
    public double AverageRating { get; set; }
    public bool IsActive { get; set; }
    public bool IsVisible { get; set; }
}
