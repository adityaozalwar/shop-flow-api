using System.ComponentModel.DataAnnotations;

namespace ShopFlowAPI.Models.DTOs.Cart;

public class UpdateCartItemRequest
{
    [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000. Use DELETE to remove an item.")]
    public int Quantity { get; set; }
}
