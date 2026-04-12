namespace DigiVault.PortalWWW.Models;

public class CourseDetailViewModel
{
    public CourseDetailDto Course { get; set; } = new CourseDetailDto();
    public IEnumerable<ReviewDto> Reviews { get; set; } = [];
}