using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Models;

namespace ShopFlowAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<CartDetails> CartDetails { get; set; }
    public DbSet<Users> Users { get; set; }
}