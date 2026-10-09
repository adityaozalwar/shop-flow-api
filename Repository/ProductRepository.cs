using ShopFlowAPI.Data;
using ShopFlowAPI.Models;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

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