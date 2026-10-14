using ShopFlowAPI.Enums;

namespace ShopFlowAPI.Models.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public int CartHeaderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public double CartPrice { get; set; }
}
