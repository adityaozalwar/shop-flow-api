using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Services.IServices;

public interface IInventoryService
{
    Task<List<InventoryDto>> GetInventoriesAsync();
    Task<InventoryDto?> GetInventoryByIdAsync(int id);
    Task<InventoryDto> CreateInventoryAsync(InventoryDto inventoryDto);
    Task<InventoryDto?> UpdateInventoryAsync(int id, InventoryDto inventoryDto);
    Task<bool> DeleteInventoryAsync(int id);
}