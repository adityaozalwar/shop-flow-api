using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Models;
using ShopFlowAPI.Models.Auth;

namespace ShopFlowAPI.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
  
        
        modelBuilder.Entity<CartHeader>()
            .HasMany(c => c.CartDetails)
            .WithOne(d => d.CartHeader)
            .HasForeignKey(d => d.CartHeaderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<CartHeader>()
            .HasOne(c => c.Order)
            .WithOne(o => o.CartHeader)
            .HasForeignKey<Order>(o => o.CartHeaderId);
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Inventory)
            .WithOne(i => i.Product)
            .HasForeignKey<Inventory>(i => i.ProductId);
        
        
        modelBuilder.Entity<CartHeader>().HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CartHeader>()
            .HasIndex(c => c.UserId)
            .IsUnique();
    
    }
}