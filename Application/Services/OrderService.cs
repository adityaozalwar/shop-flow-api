using ShopFlowAPI.Enums;
using ShopFlowAPI.Mappers;
using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Repository.IRepository;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<OrderDto>> GetOrdersAsync(string? userId)
    {
        var orders = userId is null
            ? await _unitOfWork.Order.GetAllWithCartAsync()
            : await _unitOfWork.Order.GetByUserIdAsync(userId);

        return orders.Select(order => OrderMapper.ToDto(order)).ToList();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id, string? userId)
    {
        var order = userId is null
            ? await _unitOfWork.Order.GetWithCartByIdAsync(id)
            : await _unitOfWork.Order.GetByIdForUserAsync(id, userId);

        return order is null ? null : OrderMapper.ToDto(order);
    }

    public async Task<OrderCreationResult> CreateOrderAsync(int cartHeaderId, string userId)
    {
        var cart = await _unitOfWork.Order.GetCartForUserAsync(cartHeaderId, userId);
        if (cart is null)
            return new OrderCreationResult(null, OrderCreationFailure.CartNotFound);

        if (cart.Order is not null)
            return new OrderCreationResult(null, OrderCreationFailure.AlreadyOrdered);

        if (cart.CartDetails.Count == 0)
            return new OrderCreationResult(null, OrderCreationFailure.CartEmpty);

        var order = new Order
        {
            CartHeaderId = cart.CartHeaderId,
            UserId = userId,
            OrderStatus = OrderStatus.Placed,
            PaymentStatus = PaymentStatus.Pending,
            OrderDate = DateTime.UtcNow
        };

        await _unitOfWork.Order.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return new OrderCreationResult(
            OrderMapper.ToDto(order, cart.CartPrice),
            OrderCreationFailure.None);
    }

    public async Task<OrderStatusUpdateResult> UpdateOrderStatusAsync(
        int id,
        OrderStatus orderStatus,
        PaymentStatus paymentStatus)
    {
        var order = await _unitOfWork.Order.GetForStatusUpdateAsync(id);
        if (order is null)
            return new OrderStatusUpdateResult(
                null, OrderStatusUpdateFailure.OrderNotFound, "Order not found.");

        var error = OrderStatusRules.Validate(
            order.OrderStatus, order.PaymentStatus,
            orderStatus, paymentStatus);

        if (error is not null)
            return new OrderStatusUpdateResult(
                null, OrderStatusUpdateFailure.InvalidTransition, error);

        order.OrderStatus = orderStatus;
        order.PaymentStatus = paymentStatus;

        await _unitOfWork.SaveChangesAsync();

        return new OrderStatusUpdateResult(
            OrderMapper.ToDto(order),
            OrderStatusUpdateFailure.None);
    }
}
