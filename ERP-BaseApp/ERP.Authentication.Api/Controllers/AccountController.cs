using ERP.Authentication.Jwt;
using ERP.Authentication.Jwt.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }


        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if (authenticationResponse == null)
            {
                return Unauthorized();
            }

            return authenticationResponse;
        }
    }
}
