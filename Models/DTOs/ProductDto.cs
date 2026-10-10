using System.ComponentModel.DataAnnotations;

namespace ShopFlowAPI.Models.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 2)]
    public required string ProductName { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public double Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}
