using System.Security.Claims;
using PaymentWebEntity.Entities;

namespace PaymentWebService.Services.Abstraction
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GetUserIdFromToken(string token);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
