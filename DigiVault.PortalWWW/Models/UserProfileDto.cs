namespace DigiVault.PortalWWW.Models;

public class UserProfileDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal TotalWithdrawn { get; set; }
    public int WarningsCount { get; set; }
}
