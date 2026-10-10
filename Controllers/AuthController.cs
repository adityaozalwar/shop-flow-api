using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Models.DTOs.Auth;       
using ShopFlowAPI.Services.IServices.Auth;

namespace ShopFlowAPI.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ApiResponse> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return Reply(ApiResponse.Custom(201, "Registration successful.", result));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ApiResponse> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return Reply(ApiResponse.Ok("Login successful.", result));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ApiResponse> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Reply(ApiResponse.Fail(401, "Invalid authentication identity."));

        var user = await _authService.GetCurrentUserAsync(userId);

        return user is null
            ? Reply(ApiResponse.NotFound("User not found."))
            : Reply(ApiResponse.Ok("User retrieved successfully.", user));
    }

    // Sets the real HTTP status so it matches the StatusCode in the JSON body.
    private ApiResponse Reply(ApiResponse response)
    {
        Response.StatusCode = response.StatusCode;
        return response;
    }
}