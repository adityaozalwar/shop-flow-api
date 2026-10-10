using ShopFlowAPI.Models;

namespace ShopFlowAPI.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> UpdateAsync(int id, Category category);
}