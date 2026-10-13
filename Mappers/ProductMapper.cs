using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Mappers;

public static class ProductMapper
{
    // Entity to DTO
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.CategoryName,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt,
            
            Inventory = product.Inventory == null ? null : new InventoryDto
            {
                InventoryId = product.Inventory.InventoryId,
                ProductId = product.Inventory.ProductId,
                Quantity = product.Inventory.Quantity,
                ReservedQuantity = product.Inventory.ReservedQuantity
            }
        };
    }

    // DTO to Entity
    public static Product ToEntity(ProductDto dto)
    {
        return new Product
        {
            ProductId = dto.ProductId,
            ProductName = dto.ProductName,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            CreatedAt = dto.CreatedAt,
            ModifiedAt = dto.ModifiedAt
        };
    }
}
