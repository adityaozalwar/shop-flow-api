using System.ComponentModel.DataAnnotations;

namespace ShopFlowAPI.Models.DTOs;

public class CreateOrderRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Cart header ID must be greater than zero.")]
    public int CartHeaderId { get; set; }
}
