using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Shared.Response.JWT;

namespace UserManagement.Infrastructure.Services
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;

        public JWTService(UserManager<Users> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenResponse> GenerateTokenAsync(Users user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var authClaims = GetUserClaims(user.Id, user.Email, roles);

            var token = GetToken(authClaims);
            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
                TokenExpiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60")),
                RefreshToken_ValidTill = DateTime.UtcNow.AddDays(30)
            };
        }

        public ClaimsIdentity GetUserClaims(Guid id, string email, IList<string> roles)
        {
            var authClaims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            foreach (var role in roles)
            {
                if (!string.IsNullOrWhiteSpace(role))
                    authClaims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return authClaims;
        }

        public JwtSecurityToken GetToken(ClaimsIdentity authClaims)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("Missing Jwt:Key in configuration.");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            return new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60")),
                claims: authClaims.Claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
