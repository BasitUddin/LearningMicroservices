using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagement.Domain.Entities;
using UserManagement.Shared.Response.JWT;

namespace UserManagement.Domain.Interfaces
{
    public interface IJWTService
    {
        Task<TokenResponse> GenerateTokenAsync(Users user);
        ClaimsIdentity GetUserClaims(Guid id, string email, IList<string> roles);
        JwtSecurityToken GetToken(ClaimsIdentity authClaims);
    }
}
