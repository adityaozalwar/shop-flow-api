using ShopFlowAPI.Enums;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Services;

public sealed record OrderCreationResult(OrderDto? Order, OrderCreationFailure Failure);
