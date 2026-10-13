using ShopFlowAPI.Data;
using ShopFlowAPI.Repository.IRepository;

namespace ShopFlowAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IProductRepository Product { get; }
    public ICategoryRepository Category { get; }
    public IInventoryRepository Inventory { get; }
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext dbContext)
    {
        _context = dbContext;
        Product = new ProductRepository(dbContext);
        Category = new CategoryRepository(dbContext);
        Inventory = new InventoryRepository(dbContext);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}