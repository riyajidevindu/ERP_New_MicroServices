using ERP.Authentication.Jwt.DTOs;
using ERP.Authentication.Jwt.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Authentication.Jwt
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "fdPKgjdljJSKfjkld34reofjldJPIJFkfdjl45";
        private const int JWT_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccounts;
        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccount>()
            {
                new UserAccount() { UserName="admin", Password="admin", Role="Adminstrator" },
                new UserAccount(){ UserName = "user", Password="user", Role="User" }
            };
        }


        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }

            var userAccount = _userAccounts.Where( x=> x.UserName.Equals(request.UserName) && x.Password.Equals(request.Password))
                .FirstOrDefault();

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
