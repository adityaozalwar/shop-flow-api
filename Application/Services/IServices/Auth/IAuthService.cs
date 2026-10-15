using ShopFlowAPI.Models.DTOs.Auth;

namespace ShopFlowAPI.Services.IServices.Auth;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<CurrentUserResponse?> GetCurrentUserAsync(string userId);
}