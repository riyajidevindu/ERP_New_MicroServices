using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text.Json;
using static ERP.BlazorUI.Components.Pages.Department.LoginManagement.Loginnn;

namespace ERP.BlazorUI.Components.Pages.Department.LoginManagement.Services
{
    public class CustomAuthenticationStateProviders : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly HttpClient _httpClient;
        private bool _isInitialized = false;

        public CustomAuthenticationStateProviders(ISessionStorageService sessionStorage, HttpClient httpClient)
        {
            _sessionStorage = sessionStorage;
            _httpClient = httpClient;
        }

        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    //if (!_isInitialized)
        //    //{
        //    //    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        //    //}

        //    var authResponseJson = await _sessionStorage.GetItemAsStringAsync("authResponse").ConfigureAwait(false);

        //    if (string.IsNullOrEmpty(authResponseJson))
        //     {
        //            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        //     }

        //        var authResponse = JsonSerializer.Deserialize<AuthResponse>(authResponseJson);

        //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.Token);

        //        var claims = JwtParser.ParseClaimsFromJwt(authResponse.Token).ToList();

        //        foreach (var role in authResponse.Roles)
        //        {
        //            claims.Add(new Claim(ClaimTypes.Role, role));
        //        }

        //        var identity = new ClaimsIdentity(claims, "jwtAuthType");
        //        var user = new ClaimsPrincipal(identity);


        //        return new AuthenticationState(user);
        // }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

            try
            {
                var respones = await _sessionStorage.GetItemAsync<string>("authResponse");
                var authResponseJson = await _sessionStorage.GetItemAsStringAsync("authResponse");
               
                

                if (string.IsNullOrEmpty(authResponseJson))
                {
                    return new AuthenticationState(anonymousUser);
                }

                var authResponse = JsonSerializer.Deserialize<AuthResponse>(authResponseJson);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.Token);

                var claims = JwtParser.ParseClaimsFromJwt(authResponse.Token).ToList();

                foreach (var role in authResponse.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, "jwtAuthType");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine($"Error accessing session storage: {ex.Message}");
                return new AuthenticationState(anonymousUser);
            }
        }




        public async Task InitializeAsync()
        {
            _isInitialized = true;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsAuthenticated(AuthResponse authResponse)
        {
            var claims = JwtParser.ParseClaimsFromJwt(authResponse.Token).ToList();
            var roleClaims = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // Log roles for debugging
            Console.WriteLine($"Parsed roles: {string.Join(", ", roleClaims)}");

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);

        }

        public void MarkUserAsLoggedOut()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymous));
            NotifyAuthenticationStateChanged(authState);
        }
    }

    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
