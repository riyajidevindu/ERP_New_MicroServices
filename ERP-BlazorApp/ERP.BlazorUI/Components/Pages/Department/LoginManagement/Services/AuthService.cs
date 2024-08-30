using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace ERP.BlazorUI.Components.Pages.Department.LoginManagement.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ISessionStorageService _sessionStorage;
        public AuthService(HttpClient httpClient,AuthenticationStateProvider authenticationStateProvider,ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _sessionStorage = sessionStorageService;
            
        }
        public async Task<AuthResponse?> Login(AuthRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7103/api/Auth/login", request);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthResponse>(content);
                
            }
            else
            {
                return null;
            }
        }
    }
}
