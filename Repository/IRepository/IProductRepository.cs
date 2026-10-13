using System.Linq.Expressions;
using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedProductsAsync( int pageNumber, int pageSize, string sortBy, bool descending);
    
    Task<Product?> GetByIdWithCategoryAndInventoryAsync(int id);
    
    Task<Product?> UpdateAsync(int id, Product product);
}