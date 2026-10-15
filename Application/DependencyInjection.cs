using Microsoft.Extensions.DependencyInjection;
using ShopFlowAPI.Services;
using ShopFlowAPI.Services.IServices;
using ShopFlowAPI.Services.IServices.Auth;

namespace ShopFlowAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
