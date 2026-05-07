namespace DigiVault.Intranet.Models;

public class AdminReportDto
{
    public int IdCourseReport { get; set; }
    public int IdCourse { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string ReportedBy { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public bool IsResolved { get; set; }
    public DateTime CreatedAt { get; set; }
}
