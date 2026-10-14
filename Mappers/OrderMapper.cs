using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(Order order, double? cartPrice = null)
    {
        return new OrderDto
        {
            OrderId = order.OrderId,
            CartHeaderId = order.CartHeaderId,
            OrderStatus = order.OrderStatus,
            PaymentStatus = order.PaymentStatus,
            OrderDate = order.OrderDate,
            CartPrice = cartPrice ?? order.CartHeader?.CartPrice ?? 0
        };
    }
}
