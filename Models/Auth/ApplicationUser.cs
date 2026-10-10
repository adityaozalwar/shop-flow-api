using Microsoft.AspNetCore.Identity;

namespace ShopFlowAPI.Models.Auth;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}