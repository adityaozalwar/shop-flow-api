using ShopFlowAPI.Enums;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Services;

namespace ShopFlowAPI.Services.IServices;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> GetOrdersAsync(string? userId);
    Task<OrderDto?> GetOrderByIdAsync(int id, string? userId);
    Task<OrderCreationResult> CreateOrderAsync(int cartHeaderId, string userId);
    Task<OrderStatusUpdateResult> UpdateOrderStatusAsync(int id, OrderStatus orderStatus, PaymentStatus paymentStatus);
}
