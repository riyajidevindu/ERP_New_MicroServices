using ERP.Authentication.Core.DTOs;
using ERP.Authentication.Core.Interfaces;
using ERP.Authentication.DataService;
using ERP.Authentication.DataService.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERP.Authentication.Jwt
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "fdPKgjdljJSKfjkld34reofjldJPIJFkfdjl45";
        private const int JWT_VALIDITY_MINS = 20;

        private readonly AppDbContext _context;
        public JwtTokenHandler(AppDbContext context)
        {
            _context = context;

        }


        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }
            var userAccount = _context.UserAccounts.Where(u => u.UserName == request.UserName && u.Password == request.Password).FirstOrDefault();
            //var userAccount = _repo.GetUserAccount(request.UserName, request.Password);

            if (userAccount == null)
                return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            var claimsIdentity = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                    new Claim(ClaimTypes.Role, userAccount.Role)
                });


            var signinCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var securityTokenDescripter = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signinCredentials
            };


            var jwtSecuritTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecuritTokenHandler.CreateToken(securityTokenDescripter);
            var token = jwtSecuritTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = request.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };

        }
    }
}
