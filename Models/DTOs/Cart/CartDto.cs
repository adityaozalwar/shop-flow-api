namespace ShopFlowAPI.Models.DTOs.Cart;

public class CartDto
{
    public int CartHeaderId { get; set; }
    public double CartPrice { get; set; }
    public int TotalItems { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
