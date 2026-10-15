using Microsoft.Extensions.Configuration;
using ShopFlowAPI.Data;
using ShopFlowAPI.Data.Auth;

namespace ShopFlowAPI.Infrastructure;

public static class InfrastructureInitializer
{
    public static async Task InitializeInfrastructureAsync(
        this IServiceProvider services,
        IConfiguration configuration)
    {
        await IdentitySeeder.SeedRolesAsync(services);
        await IdentitySeeder.SeedAdminAsync(services, configuration);
        await DbSeeder.SeedDemoDataAsync(services, configuration);
    }
}
