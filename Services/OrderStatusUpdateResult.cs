using ShopFlowAPI.Enums;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Services;

public sealed record OrderStatusUpdateResult(
    OrderDto? Order,
    OrderStatusUpdateFailure Failure,
    string? Message = null);
