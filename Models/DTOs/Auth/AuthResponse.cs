namespace ShopFlowAPI.Models.DTOs.Auth;

public sealed record AuthResponse( 
    string AccessToken, 
    DateTime ExpiresAtUtc, 
    string UserId, 
    string Email, 
    string[] Roles);