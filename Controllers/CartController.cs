using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Models.DTOs.Cart;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // GET : /api/cart
    [HttpGet]
    public async Task<ApiResponse> GetCart()
        => ApiResponse.Ok("Fetched cart successfully", await _cartService.GetCartAsync(GetUserId()));

    // POST : /api/cart/items
    [HttpPost("items")]
    public async Task<ApiResponse> AddItem([FromBody] AddToCartRequest request)
        => ApiResponse.Ok("Item added to cart", await _cartService.AddItemAsync(GetUserId(), request));

    // PUT : /api/cart/items/{productId}
    [HttpPut("items/{productId:int}")]
    public async Task<ApiResponse> UpdateItem([FromRoute] int productId, [FromBody] UpdateCartItemRequest request)
        => ApiResponse.Ok("Cart item updated", await _cartService.UpdateItemAsync(GetUserId(), productId, request));

    // DELETE : /api/cart/items/{productId}
    [HttpDelete("items/{productId:int}")]
    public async Task<ApiResponse> RemoveItem([FromRoute] int productId)
        => ApiResponse.Ok("Item removed from cart", await _cartService.RemoveItemAsync(GetUserId(), productId));

    // DELETE : /api/cart
    [HttpDelete]
    public async Task<ApiResponse> ClearCart()
    {
        await _cartService.ClearCartAsync(GetUserId());
        return ApiResponse.Ok("Cart cleared", null);
    }

    private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? throw new UnauthorizedAccessException("Invalid authentication identity.");
}
