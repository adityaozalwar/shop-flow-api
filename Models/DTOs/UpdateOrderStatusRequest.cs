using System.ComponentModel.DataAnnotations;
using ShopFlowAPI.Enums;

namespace ShopFlowAPI.Models.DTOs;

public class UpdateOrderStatusRequest
{
    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus OrderStatus { get; set; }

    [EnumDataType(typeof(PaymentStatus))]
    public PaymentStatus PaymentStatus { get; set; }
}
