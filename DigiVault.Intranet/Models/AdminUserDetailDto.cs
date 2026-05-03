namespace DigiVault.Intranet.Models;

public class AdminUserDetailDto
{
    public int IdUser { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal TotalWithdrawn { get; set; }
    public int WarningsCount { get; set; }
    public bool IsActive { get; set; }
    public List<AdminUserCourseDto> CreatedCourses { get; set; } = [];
    public List<AdminUserCourseDto> PurchasedCourses { get; set; } = [];
}

public class AdminUserCourseDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
