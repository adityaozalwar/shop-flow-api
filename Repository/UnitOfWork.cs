using ShopFlowAPI.Data;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IProductRepository Product { get; }
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext dbContext)
    {
        Product = new ProductRepository(dbContext);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}