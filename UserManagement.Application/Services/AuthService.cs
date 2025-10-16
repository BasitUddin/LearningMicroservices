using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Shared.Request.Auth;
using UserManagement.Shared.Response.Auth;

namespace UserManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IJWTService _jWTService;
        private readonly SignInManager<Users> _signInManager;

        public AuthService(UserManager<Users> userManager, IJWTService jWTService, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _jWTService = jWTService;
            _signInManager = signInManager;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Invalid email or password.");

            var password = await _userManager.CheckPasswordAsync(user, request.Password);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid email or password.");

            var token = await _jWTService.GenerateTokenAsync(user);

            //return Ok(new { token, refreshToken });
            return new LoginResponse
            {
                Token = token.Token,
                Expiration = token.TokenExpiry,
                UserId = user.Id.ToString(),
                Email = user.Email ?? string.Empty
            };
        }

        public async Task<string> Register(RegisterRequest model)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    throw new Exception("User already exists!");

                var user = new Users
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    FullName = "System Administrator",
                    NormalizedUserName = model.Email.ToUpper(),
                    NormalizedEmail = model.Email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "03001234567",
                    PhoneNumberConfirmed = true,
                    Active = true,
                    Deleted = false,
                    CreatedBy = Guid.Empty,
                    ModifiedBy = Guid.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    throw new Exception(result.Errors.ToString());

                return "User created successfully!";
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
