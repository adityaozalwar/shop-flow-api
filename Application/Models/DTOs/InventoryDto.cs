using System.ComponentModel.DataAnnotations;

namespace ShopFlowAPI.Models.DTOs;

public class InventoryDto
{
    public int InventoryId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Product ID must be greater than zero.")]
    public int ProductId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Reserved quantity cannot be negative.")]
    public int ReservedQuantity { get; set; }
}
