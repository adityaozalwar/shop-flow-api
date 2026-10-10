using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Mappers;

public static class InventoryMapper
{
    public static InventoryDto ToDto(Inventory inventory)
    {
        return new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            ProductId = inventory.ProductId,
            Quantity = inventory.Quantity,
            ReservedQuantity = inventory.ReservedQuantity
        };
    }

    public static Inventory ToEntity(InventoryDto inventoryDto)
    {
        return new Inventory
        {
            InventoryId = inventoryDto.InventoryId,
            ProductId = inventoryDto.ProductId,
            Quantity = inventoryDto.Quantity,
            ReservedQuantity = inventoryDto.ReservedQuantity
        };
    }
}