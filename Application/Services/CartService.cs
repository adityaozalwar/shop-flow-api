using ShopFlowAPI.Exceptions;
using ShopFlowAPI.Mappers;
using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs.Cart;
using ShopFlowAPI.Repository.IRepository;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> GetCartAsync(string userId)
    {
        var cart = await _unitOfWork.Cart.GetByUserIdWithDetailsReadOnlyAsync(userId);

        return cart is null ? CartMapper.Empty() : CartMapper.ToDto(cart);
    }

    public async Task<CartDto> AddItemAsync(string userId, AddToCartRequest request)
    {
        var product = await _unitOfWork.Cart.GetProductWithInventoryAsync(request.ProductId)
                      ?? throw new NotFoundException("Product not found.");

        var now = DateTime.UtcNow;

        var cart = await _unitOfWork.Cart.GetByUserIdWithDetailsAsync(userId);
        if (cart is null)
        {
            cart = new CartHeader
            {
                UserId = userId,
                CreatedAt = now,
                ModifiedAt = now
            };
            await _unitOfWork.Cart.AddAsync(cart);
        }

        var line = cart.CartDetails.FirstOrDefault(d => d.ProductId == request.ProductId);
        var newQuantity = (line?.CartQuantity ?? 0) + request.Quantity;

        EnsureStock(product, newQuantity);

        if (line is null)
        {
            cart.CartDetails.Add(new CartDetails
            {
                ProductId = product.ProductId,
                Product = product,
                CartQuantity = newQuantity,
                CreatedAt = now,
                ModifiedAt = now
            });
        }
        else
        {
            line.CartQuantity = newQuantity;
            line.ModifiedAt = now;
        }

        RecalculateTotals(cart, now);
        await _unitOfWork.SaveChangesAsync();

        return CartMapper.ToDto(cart);
    }

    public async Task<CartDto> UpdateItemAsync(string userId, int productId, UpdateCartItemRequest request)
    {
        var cart = await _unitOfWork.Cart.GetByUserIdWithDetailsAsync(userId)
                   ?? throw new NotFoundException("Cart not found.");

        var line = cart.CartDetails.FirstOrDefault(d => d.ProductId == productId)
                   ?? throw new NotFoundException("Item not found in cart.");

        var product = await _unitOfWork.Cart.GetProductWithInventoryAsync(productId)
                      ?? throw new NotFoundException("Product not found.");

        EnsureStock(product, request.Quantity);

        var now = DateTime.UtcNow;
        line.CartQuantity = request.Quantity;
        line.ModifiedAt = now;

        RecalculateTotals(cart, now);
        await _unitOfWork.SaveChangesAsync();

        return CartMapper.ToDto(cart);
    }

    public async Task<CartDto> RemoveItemAsync(string userId, int productId)
    {
        var cart = await _unitOfWork.Cart.GetByUserIdWithDetailsAsync(userId)
                   ?? throw new NotFoundException("Cart not found.");

        var line = cart.CartDetails.FirstOrDefault(d => d.ProductId == productId)
                   ?? throw new NotFoundException("Item not found in cart.");

        cart.CartDetails.Remove(line);
        _unitOfWork.Cart.RemoveDetail(line);

        RecalculateTotals(cart, DateTime.UtcNow);
        await _unitOfWork.SaveChangesAsync();

        return CartMapper.ToDto(cart);
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await _unitOfWork.Cart.GetByUserIdWithDetailsAsync(userId);
        if (cart is null) return;

        foreach (var line in cart.CartDetails.ToList())
            _unitOfWork.Cart.RemoveDetail(line);

        cart.CartDetails.Clear();
        RecalculateTotals(cart, DateTime.UtcNow);

        await _unitOfWork.SaveChangesAsync();
    }

    // ---- helpers ----

    private static void EnsureStock(Product product, int requestedQuantity)
    {
        var inventory = product.Inventory
                        ?? throw new BadRequestException($"'{product.ProductName}' is not available.");

        var available = inventory.Quantity - inventory.ReservedQuantity;

        if (requestedQuantity > available)
            throw new BadRequestException(
                $"Only {Math.Max(available, 0)} unit(s) of '{product.ProductName}' available.");
    }

    private static void RecalculateTotals(CartHeader cart, DateTime now)
    {
        cart.CartPrice = cart.CartDetails.Sum(d => d.Product.Price * d.CartQuantity);
        cart.ModifiedAt = now;
    }
}
