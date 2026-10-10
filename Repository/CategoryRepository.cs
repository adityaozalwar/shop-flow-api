using ShopFlowAPI.Data;
using ShopFlowAPI.Models;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public async Task<Category?> UpdateAsync(int id, Category category)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing is null) return null;

        existing.CategoryName = category.CategoryName;
        existing.ModifiedAt = DateTime.UtcNow;

        return existing; 
    }
}