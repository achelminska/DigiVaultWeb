namespace DigiVault.Intranet.Models;

public class AdminCourseDetailDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsVisible { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int ReportsCount { get; set; }
    public int SalesCount { get; set; }
    public double AverageRating { get; set; }
}
