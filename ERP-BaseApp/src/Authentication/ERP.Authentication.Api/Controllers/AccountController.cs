using ERP.Authentication.Core.DTOs;
using ERP.Authentication.Core.Interfaces;
using ERP.Authentication.DataService;
using ERP.Authentication.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERP.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest request)
        {
            //var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);

            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Unauthorized(); ;
            }

            var userAccount = _context.UserAccounts.Where(u => u.UserName == request.UserName && u.Password == request.Password).FirstOrDefault();
            
            if (userAccount == null)
                return Unauthorized();

            var key = _configuration["Jwt:SecretKey"];
            //var key = "fdPKgjdljJSKfjkld34reofjldJPIJFkfdjl45";
            //var mins = 20;
            var mins = _configuration.GetValue<int>("Jwt:JWT_VALIDITY_MINS");

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(mins);            
            var tokenKey = Encoding.ASCII.GetBytes(key);

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

            var authenticationResponse = new AuthenticationResponse
            {
                UserName = request.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };


            return authenticationResponse;
        }
    }
}
