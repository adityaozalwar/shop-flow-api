namespace ShopFlowAPI.Models.DTOs.Cart;

public class CartItemDto
{
    public int CartDetailsId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
    public int CartQuantity { get; set; }
    public double LineTotal { get; set; }
}
