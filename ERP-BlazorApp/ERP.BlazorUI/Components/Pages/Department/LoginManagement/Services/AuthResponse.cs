namespace ERP.BlazorUI.Components.Pages.Department.LoginManagement.Services
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Roles { get; set; }

        public string FirstName { get; set; }

        
    }
}