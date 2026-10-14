using System.ComponentModel.DataAnnotations;

namespace ShopFlowAPI.Models.DTOs.Cart;

public class AddToCartRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid product.")]
    public int ProductId { get; set; }

    [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
    public int Quantity { get; set; } = 1;
}
