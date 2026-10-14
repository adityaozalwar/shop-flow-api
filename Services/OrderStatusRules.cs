using ShopFlowAPI.Enums;

namespace ShopFlowAPI.Services;

/// <summary>
/// Single place that decides which order/payment status changes are allowed.
/// </summary>
public static class OrderStatusRules
{
    private static readonly Dictionary<OrderStatus, OrderStatus[]> OrderTransitions = new()
    {
        [OrderStatus.Pending]   = new[] { OrderStatus.Placed, OrderStatus.Cancelled },
        [OrderStatus.Placed]    = new[] { OrderStatus.Shipped, OrderStatus.Cancelled },
        [OrderStatus.Shipped]   = new[] { OrderStatus.Delivered },
        [OrderStatus.Delivered] = Array.Empty<OrderStatus>(),
        [OrderStatus.Cancelled] = Array.Empty<OrderStatus>()
    };

    private static readonly Dictionary<PaymentStatus, PaymentStatus[]> PaymentTransitions = new()
    {
        [PaymentStatus.Pending]    = new[] { PaymentStatus.InProgress, PaymentStatus.Succeeded, PaymentStatus.Failed },
        [PaymentStatus.InProgress] = new[] { PaymentStatus.Succeeded, PaymentStatus.Failed },
        [PaymentStatus.Failed]     = new[] { PaymentStatus.Pending, PaymentStatus.InProgress, PaymentStatus.Succeeded },
        [PaymentStatus.Succeeded]  = Array.Empty<PaymentStatus>()   // no refunds modelled yet
    };

    /// <returns>An error message, or null when the change is allowed.</returns>
    public static string? Validate(
        OrderStatus currentOrder, PaymentStatus currentPayment,
        OrderStatus newOrder, PaymentStatus newPayment)
    {
        // Staying on the same value is always fine, so admin can change just one of the two.
        if (newOrder != currentOrder && !OrderTransitions[currentOrder].Contains(newOrder))
            return $"Cannot change order status from {currentOrder} to {newOrder}.";

        if (newPayment != currentPayment && !PaymentTransitions[currentPayment].Contains(newPayment))
            return $"Cannot change payment status from {currentPayment} to {newPayment}.";

        if (currentOrder == OrderStatus.Cancelled && newPayment != currentPayment)
            return "Payment status of a cancelled order cannot be changed.";

        // Business rule: nothing leaves the warehouse unpaid.
        // (Remove this block if you want to allow cash on delivery.)
        if (newOrder is OrderStatus.Shipped or OrderStatus.Delivered
            && newPayment != PaymentStatus.Succeeded)
            return "Payment must be Succeeded before an order can be shipped or delivered.";

        return null;
    }
}
