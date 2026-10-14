using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Models;

namespace ShopFlowAPI.Data;

public static class DbSeeder
{
    /// <summary>
    /// Adds demo categories, products and inventory once, when the category table is empty.
    /// Enabled with the config key SeedDemoData=true.
    /// </summary>
    public static async Task SeedDemoDataAsync(IServiceProvider services, IConfiguration config)
    {
        if (!config.GetValue<bool>("SeedDemoData"))
            return;

        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (await db.Categories.AnyAsync())
            return;

        var now = DateTime.UtcNow;

        var catalog = new Dictionary<string, (string Name, double Price, int Stock)[]>
        {
            ["Electronics"] = new[]
            {
                ("Wireless Mouse", 799.0, 50),
                ("Mechanical Keyboard", 2499.0, 30),
                ("USB-C Hub", 1299.0, 40),
                ("Noise-Cancelling Headphones", 5999.0, 15)
            },
            ["Books"] = new[]
            {
                ("Programming Fundamentals", 649.0, 60),
                ("Database Design Basics", 749.0, 45),
                ("Web API Cookbook", 899.0, 25),
                ("Software Architecture Notes", 999.0, 20)
            },
            ["Home & Office"] = new[]
            {
                ("Desk Lamp", 1199.0, 35),
                ("Ergonomic Chair Cushion", 1499.0, 25),
                ("Laptop Stand", 1099.0, 40),
                ("Notebook Set", 349.0, 100)
            }
        };

        foreach (var (categoryName, items) in catalog)
        {
            var category = new Category
            {
                CategoryName = categoryName,
                CreatedAt = now,
                ModifiedAt = now
            };

            foreach (var (name, price, stock) in items)
            {
                category.Products.Add(new Product
                {
                    ProductName = name,
                    Price = price,
                    CreatedAt = now,
                    ModifiedAt = now,
                    Inventory = new Inventory
                    {
                        Quantity = stock,
                        ReservedQuantity = 0,
                        CreatedAt = now,
                        ModifiedAt = now
                    }
                });
            }

            db.Categories.Add(category);
        }

        await db.SaveChangesAsync();
    }
}