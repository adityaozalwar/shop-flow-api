using ShopFlowAPI.Models.Auth;

namespace ShopFlowAPI.Data.Auth;

using Microsoft.AspNetCore.Identity;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in new[] { "User", "Admin" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create role '{role}': " + 
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
    
    public static async Task SeedAdminAsync(IServiceProvider services, IConfiguration config)
    {
        var email = config["Admin:Email"];
        var password = config["Admin:Password"];

        // No admin configured -> skip silently.
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return;

        using var scope = services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = config["Admin:FullName"] ?? "Administrator"
            };

            var create = await userManager.CreateAsync(user, password);
            if (!create.Succeeded)
                throw new InvalidOperationException(
                    "Failed to create admin: " +
                    string.Join(", ", create.Errors.Select(e => e.Description)));
        }

        if (!await userManager.IsInRoleAsync(user, "Admin"))
            await userManager.AddToRolesAsync(user, new[] { "User", "Admin" });
    }
}