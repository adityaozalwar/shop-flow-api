using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Data;
using ShopFlowAPI.Models;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class CartRepository : Repository<CartHeader>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context) { }

    public async Task<CartHeader?> GetByUserIdWithDetailsAsync(string userId)
        => await _dbSet
            .Include(c => c.CartDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<CartHeader?> GetByUserIdWithDetailsReadOnlyAsync(string userId)
        => await _dbSet.AsNoTracking()
            .Include(c => c.CartDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<Product?> GetProductWithInventoryAsync(int productId)
        => await _context.Products
            .Include(p => p.Inventory)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

    public void RemoveDetail(CartDetails detail)
    {
        _context.CartDetails.Remove(detail);
    }
}
