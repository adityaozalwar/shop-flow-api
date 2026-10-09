namespace ShopFlowAPI.Repository.IRepository;

public interface IUnitOfWork
{
    IProductRepository Product { get; }
    
    Task SaveChangesAsync();
}