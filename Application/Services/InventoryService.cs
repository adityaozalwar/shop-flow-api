using ShopFlowAPI.Mappers;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Repository.IRepository;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Services;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public InventoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<InventoryDto>> GetInventoriesAsync()
    {
        var inventories = await _unitOfWork.Inventory.GetAllAsync();

        return inventories .Select(InventoryMapper.ToDto) .ToList();
    }

    public async Task<InventoryDto?> GetInventoryByIdAsync(int id)
    {
        var inventory = await _unitOfWork.Inventory.GetByIdAsync(id);
        
        if(inventory == null) return null;
        
        return InventoryMapper.ToDto(inventory);
    }

    public async Task<InventoryDto> CreateInventoryAsync( InventoryDto inventoryDto)
    {
        var inventory = InventoryMapper.ToEntity(inventoryDto);

        inventory.CreatedAt = DateTime.UtcNow;
        inventory.ModifiedAt = DateTime.UtcNow;

        await _unitOfWork.Inventory.AddAsync(inventory);
        await _unitOfWork.SaveChangesAsync();

        return InventoryMapper.ToDto(inventory);
    }

    public async Task<InventoryDto?> UpdateInventoryAsync( int id, InventoryDto inventoryDto)
    {
        var inventory = InventoryMapper.ToEntity(inventoryDto);
        inventory.ModifiedAt = DateTime.UtcNow;

        var updatedInventory = await _unitOfWork.Inventory.UpdateAsync(id, inventory);

        if (updatedInventory == null)
            return null;

        await _unitOfWork.SaveChangesAsync();

        return InventoryMapper.ToDto(updatedInventory);
    }

    public async Task<bool> DeleteInventoryAsync(int id)
    {
        var inventory = await _unitOfWork.Inventory.GetByIdAsync(id);

        if (inventory == null)
            return false;

        _unitOfWork.Inventory.Remove(inventory);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}