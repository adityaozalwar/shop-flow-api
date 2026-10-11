using System.Linq.Expressions;
using ShopFlowAPI.Models;

namespace ShopFlowAPI.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedProductsAsync( int pageNumber, int pageSize, string sortBy, bool descending);

    Task<Product?> UpdateAsync(int id, Product product);
}