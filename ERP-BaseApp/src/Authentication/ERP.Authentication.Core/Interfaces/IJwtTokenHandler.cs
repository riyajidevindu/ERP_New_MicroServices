using ERP.Authentication.Core.DTOs;

namespace ERP.Authentication.Core.Interfaces
{
    public interface IJwtTokenHandler
    {
        AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request);
    }
}