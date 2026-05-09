namespace DigiVault.PortalWWW.Models;

public class AccountViewModel
{
    public UserProfileDto Profile { get; set; } = new();
    public List<CourseListDto> MyCourses { get; set; } = [];
    public List<OrderHistoryDto> Orders { get; set; } = [];
}
