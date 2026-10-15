using System.ComponentModel.DataAnnotations;

namespace ShopFlowAPI.Models.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
    public required string CategoryName { get; set; }
}