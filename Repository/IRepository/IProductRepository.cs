using ShopFlowAPI.Models;

namespace ShopFlowAPI.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> UpdateAsync(int id, Product product);
}