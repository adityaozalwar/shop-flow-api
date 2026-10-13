using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Data;
using ShopFlowAPI.Models;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedProductsAsync(int pageNumber, int pageSize, string sortBy, bool descending)
    {
        if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be at least 1.");
        if (pageSize < 1 || pageSize > 100) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be between 1 and 100.");

        IQueryable<Product> query = _dbSet.AsNoTracking();
        int totalCount = await query.CountAsync();

        IOrderedQueryable<Product> orderedQuery = (sortBy?.Trim().ToLowerInvariant(), descending) switch
        {
            ("productid", false) => query.OrderBy(p => p.ProductId),
            ("productid", true) => query.OrderByDescending(p => p.ProductId),
            ("productname", false) => query.OrderBy(p => p.ProductName).ThenBy(p => p.ProductId),
            ("productname", true) => query.OrderByDescending(p => p.ProductName).ThenBy(p => p.ProductId),
            ("price", false) => query.OrderBy(p => p.Price).ThenBy(p => p.ProductId),
            ("price", true) => query.OrderByDescending(p => p.Price).ThenBy(p => p.ProductId),
            ("categoryid", false) => query.OrderBy(p => p.CategoryId).ThenBy(p => p.ProductId),
            ("categoryid", true) => query.OrderByDescending(p => p.CategoryId).ThenBy(p => p.ProductId),
            ("createdat", false) => query.OrderBy(p => p.CreatedAt).ThenBy(p => p.ProductId),
            ("createdat", true) => query.OrderByDescending(p => p.CreatedAt).ThenBy(p => p.ProductId),
            ("modifiedat", false) => query.OrderBy(p => p.ModifiedAt).ThenBy(p => p.ProductId),
            ("modifiedat", true) => query.OrderByDescending(p => p.ModifiedAt).ThenBy(p => p.ProductId),
            _ => throw new ArgumentException($"Unsupported sort field '{sortBy}'.", nameof(sortBy))
        };

        var items = await orderedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, totalCount);
    }

    public async Task<Product?> GetByIdWithCategoryAndInventoryAsync(int id)
    => await _dbSet.AsNoTracking()
            .Include(c=>c.Category)
            .Include(i=>i.Inventory)
            .FirstOrDefaultAsync(p => p.ProductId == id);

    public async Task<Product?> UpdateAsync(int id, Product product)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return null;

        existing.ProductName = product.ProductName;
        existing.Price = product.Price;
        existing.CategoryId = product.CategoryId;
        existing.ModifiedAt = product.ModifiedAt;

        return existing; 
    }
}