namespace DigiVault.PortalWWW.Models;

public class CourseListDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int RatingsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}