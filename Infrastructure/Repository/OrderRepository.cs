using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Data;
using ShopFlowAPI.Models;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Order>> GetAllWithCartAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .Include(order => order.CartHeader)
            .OrderByDescending(order => order.OrderDate)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(order => order.CartHeader)
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetWithCartByIdAsync(int id)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(order => order.CartHeader)
            .FirstOrDefaultAsync(order => order.OrderId == id);
    }

    public async Task<Order?> GetByIdForUserAsync(int id, string userId)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(order => order.CartHeader)
            .FirstOrDefaultAsync(order => order.OrderId == id && order.UserId == userId);
    }

    public async Task<Order?> GetForStatusUpdateAsync(int id)
    {
        return await _dbSet
            .Include(order => order.CartHeader)
            .FirstOrDefaultAsync(order => order.OrderId == id);
    }

    public async Task<CartHeader?> GetCartForUserAsync(int cartHeaderId, string userId)
    {
        return await _context.CartHeaders
            .AsNoTracking()
            .Include(cart => cart.CartDetails)
            .Include(cart => cart.Order)
            .FirstOrDefaultAsync(cart => cart.CartHeaderId == cartHeaderId && cart.UserId == userId);
    }
}
