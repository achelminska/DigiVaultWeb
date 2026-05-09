namespace DigiVault.PortalWWW.Models;

public class CourseDetailViewModel
{
    public CourseDetailDto Course { get; set; } = new CourseDetailDto();
    public IEnumerable<ReviewDto> Reviews { get; set; } = [];
    public bool IsInCart { get; set; }
    public bool IsInWishlist { get; set; }
    public bool IsPurchased { get; set; }
}