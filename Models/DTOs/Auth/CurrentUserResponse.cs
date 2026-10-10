namespace ShopFlowAPI.Models.DTOs.Auth;


public sealed record CurrentUserResponse(
    string UserId,
    string? FullName,
    string? Email,
    string[] Roles);