using ShopFlowAPI.Models;

namespace ShopFlowAPI.Repository.IRepository;

public interface IOrderRepository : IRepository<Order>
{
    Task<IReadOnlyList<Order>> GetAllWithCartAsync();
    Task<IReadOnlyList<Order>> GetByUserIdAsync(string userId);
    Task<Order?> GetWithCartByIdAsync(int id);
    Task<Order?> GetByIdForUserAsync(int id, string userId);
    Task<Order?> GetForStatusUpdateAsync(int id);
    Task<CartHeader?> GetCartForUserAsync(int cartHeaderId, string userId);
}
