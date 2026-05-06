namespace DigiVault.Intranet.Models;

public class AdminOrderDto
{
    public int IdOrder { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ItemsCount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}
