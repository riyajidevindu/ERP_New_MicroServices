using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.Core.DTOs
{
    public class LoginRequest
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Create a DTO for login response
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public IList<string> Roles { get; set; }

        public string FirstName { get; set; }
    }
}
