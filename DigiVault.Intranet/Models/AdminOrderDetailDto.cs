namespace DigiVault.Intranet.Models;

public class AdminOrderDetailDto
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public int ItemsCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<AdminOrderItemDto> OrderItems { get; set; } = [];
}

public class AdminOrderItemDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
