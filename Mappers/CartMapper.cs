using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs.Cart;

namespace ShopFlowAPI.Mappers;

public static class CartMapper
{
    public static CartItemDto ToDto(CartDetails detail)
    {
        return new CartItemDto
        {
            CartDetailsId = detail.CartDetailsId,
            ProductId = detail.ProductId,
            ProductName = detail.Product.ProductName,
            UnitPrice = detail.Product.Price,
            CartQuantity = detail.CartQuantity,
            LineTotal = detail.Product.Price * detail.CartQuantity
        };
    }

    public static CartDto ToDto(CartHeader header)
    {
        return new CartDto
        {
            CartHeaderId = header.CartHeaderId,
            CartPrice = header.CartPrice,
            TotalItems = header.CartDetails.Sum(d => d.CartQuantity),
            Items = header.CartDetails
                .OrderBy(d => d.CartDetailsId)
                .Select(ToDto)
                .ToList(),
            CreatedAt = header.CreatedAt,
            ModifiedAt = header.ModifiedAt
        };
    }

    public static CartDto Empty() => new();
}
