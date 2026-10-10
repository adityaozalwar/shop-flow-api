using ShopFlowAPI.Data;
using ShopFlowAPI.Models;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class InventoryRepository : Repository<Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext context) : base(context) { }

    public async Task<Inventory?> UpdateAsync(int id, Inventory inventory)
    {
        var existing = await _dbSet.FindAsync(id);

        if (existing is null)
            return null;

        existing.ProductId = inventory.ProductId;
        existing.Quantity = inventory.Quantity;
        existing.ReservedQuantity = inventory.ReservedQuantity;
        existing.ModifiedAt = DateTime.UtcNow;

        return existing;
    }

}