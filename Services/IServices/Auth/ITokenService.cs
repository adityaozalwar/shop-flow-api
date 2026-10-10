using ShopFlowAPI.Models.Auth;

namespace ShopFlowAPI.Services.IServices.Auth;

public interface ITokenService
{
    string GenerateAccessToken( ApplicationUser user, IList<string> roles);
}