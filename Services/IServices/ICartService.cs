using ShopFlowAPI.Models.DTOs.Cart;

namespace ShopFlowAPI.Services.IServices;

public interface ICartService
{
    Task<CartDto> GetCartAsync(string userId);
    Task<CartDto> AddItemAsync(string userId, AddToCartRequest request);
    Task<CartDto> UpdateItemAsync(string userId, int productId, UpdateCartItemRequest request);
    Task<CartDto> RemoveItemAsync(string userId, int productId);
    Task ClearCartAsync(string userId);
}
