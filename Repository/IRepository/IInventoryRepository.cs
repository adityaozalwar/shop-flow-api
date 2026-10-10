using ShopFlowAPI.Models;

namespace ShopFlowAPI.Repository.IRepository;

public interface IInventoryRepository : IRepository<Inventory>
{
    Task<Inventory?> UpdateAsync(int id, Inventory inventory);
}