using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Shared.Request.Auth;
using UserManagement.Shared.Response.Auth;

namespace UserManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<string> Register(RegisterRequest model);
    }
}
