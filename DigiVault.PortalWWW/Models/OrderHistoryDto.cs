namespace DigiVault.PortalWWW.Models;

public class OrderHistoryDto
{
    public int IdOrder { get; set; }
    public decimal TotalPrice { get; set; }
    public int ItemsCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
}

public class OrderItemDto
{
    public int IdCourse { get; set; }
    public decimal Price { get; set; }
    public string Title { get; set; } = string.Empty;
}
