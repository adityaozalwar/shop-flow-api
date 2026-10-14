using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlowAPI.Enums;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Services.IServices;
using static ShopFlowAPI.Enums.OrderCreationFailure;

namespace ShopFlowAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var userId = User.IsInRole("Admin") ? null : GetUserId();
        if (userId is null && !User.IsInRole("Admin"))
            return Reply(ApiResponse.Fail(401, "Invalid authentication identity."));

        var orders = await _orderService.GetOrdersAsync(userId);
        return Reply(ApiResponse.Ok("Orders retrieved successfully.", orders));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById([FromRoute] int id)
    {
        var userId = User.IsInRole("Admin") ? null : GetUserId();
        if (userId is null && !User.IsInRole("Admin"))
            return Reply(ApiResponse.Fail(401, "Invalid authentication identity."));

        var order = await _orderService.GetOrderByIdAsync(id, userId);
        return order is null
            ? Reply(ApiResponse.NotFound("Order not found."))
            : Reply(ApiResponse.Ok("Order retrieved successfully.", order));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
            return Reply(ApiResponse.Fail(401, "Invalid authentication identity."));

        var result = await _orderService.CreateOrderAsync(request.CartHeaderId, userId);
        return result.Failure switch
        {
            None => Reply(ApiResponse.Custom(201, "Order placed successfully.", result.Order)),
            CartNotFound => Reply(ApiResponse.NotFound("Cart not found.")),
            CartEmpty => Reply(ApiResponse.Fail(409, "Cannot place an order for an empty cart.")),
            AlreadyOrdered => Reply(ApiResponse.Fail(409, "This cart already has an order.")),
            _ => throw new InvalidOperationException($"Unexpected order creation result: {result.Failure}.")
        };
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus([FromRoute] int id, [FromBody] UpdateOrderStatusRequest request)
    {
        if (!Enum.IsDefined(request.OrderStatus) || !Enum.IsDefined(request.PaymentStatus))
            return Reply(ApiResponse.Fail(400, "Invalid order or payment status."));

        var result = await _orderService.UpdateOrderStatusAsync(
            id,
            request.OrderStatus,
            request.PaymentStatus);

        // Fully qualified on purpose: both failure enums have a member called None.
        return result.Failure switch
        {
            OrderStatusUpdateFailure.None =>
                Reply(ApiResponse.Ok("Order status updated successfully.", result.Order)),
            OrderStatusUpdateFailure.OrderNotFound =>
                Reply(ApiResponse.NotFound("Order not found.")),
            OrderStatusUpdateFailure.InvalidTransition =>
                Reply(ApiResponse.Fail(409, result.Message ?? "Invalid status change.")),
            _ => throw new InvalidOperationException($"Unexpected order status result: {result.Failure}.")
        };
    }

    private string? GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

    private IActionResult Reply(ApiResponse response)
    {
        return StatusCode(response.StatusCode, response);
    }
}
