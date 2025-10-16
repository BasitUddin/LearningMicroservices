using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Shared.Response.JWT
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshToken_ValidTill { get; set; }
        public DateTime TokenExpiry { get; set; }
    }
}
