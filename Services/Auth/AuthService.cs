using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ShopFlowAPI.Exceptions;
using ShopFlowAPI.Models.Auth;
using ShopFlowAPI.Models.DTOs.Auth;          // adjust to wherever your auth DTOs live
using ShopFlowAPI.Services.IServices.Auth;

namespace ShopFlowAPI.Services.Auth;

public sealed class AuthService : IAuthService
{
    private const string DefaultRole = "User";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var email = request.Email.Trim();

        if (await _userManager.FindByEmailAsync(email) is not null)
            throw new Exception("Email already exists");

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = request.FullName.Trim()
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            throw new AuthValidationException(createResult.Errors.Select(e => e.Description));

        // Role is always assigned server-side; clients can never choose it.
        var roleResult = await _userManager.AddToRoleAsync(user, DefaultRole);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user); // no half-registered accounts
            throw new AuthValidationException(roleResult.Errors.Select(e => e.Description));
        }

        return await BuildAuthResponseAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.Trim());

        // Same exception for "no such user" and "wrong password" to avoid user enumeration.
        if (user is null)
            throw new InvalidCredentialsException();

        // lockoutOnFailure: true makes the lockout policy in Program.cs actually work.
        var result = await _signInManager.CheckPasswordSignInAsync(
            user, request.Password, lockoutOnFailure: true);

        if (result.IsLockedOut)
            throw new Exception("User is locked out");

        if (!result.Succeeded)
            throw new InvalidCredentialsException();

        return await BuildAuthResponseAsync(user);
    }

    public async Task<CurrentUserResponse?> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new CurrentUserResponse(user.Id, user.FullName, user.Email, roles.ToArray());
    }

    private async Task<AuthResponse> BuildAuthResponseAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        // Computed just before token generation, from the same JwtOptions the token uses.
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);
        var accessToken = _tokenService.GenerateAccessToken(user, roles);

        return new AuthResponse(
            accessToken,
            expiresAt,
            user.Id,
            user.Email ?? string.Empty,
            roles.ToArray());
    }
}