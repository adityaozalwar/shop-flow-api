namespace ShopFlowAPI.Repository.IRepository;

public interface IUnitOfWork
{
    IProductRepository Product { get; }
    ICategoryRepository Category { get; }
    IInventoryRepository Inventory { get; }
    ICartRepository Cart { get; }
    IOrderRepository Order { get; }
    
    Task SaveChangesAsync();
}