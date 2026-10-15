using ShopFlowAPI.Models;

namespace ShopFlowAPI.Repository.IRepository;

public interface ICartRepository : IRepository<CartHeader>
{
    /// <summary>Cart of a user with its lines and each line's product. Tracked, so it can be modified.</summary>
    Task<CartHeader?> GetByUserIdWithDetailsAsync(string userId);

    /// <summary>Read-only version of the above.</summary>
    Task<CartHeader?> GetByUserIdWithDetailsReadOnlyAsync(string userId);

    /// <summary>Tracked product with its inventory (needed for stock checks).</summary>
    Task<Product?> GetProductWithInventoryAsync(int productId);

    void RemoveDetail(CartDetails detail);
}
