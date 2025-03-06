using PaymentWebEntity.Entities;

namespace PaymentWebService.Services.Abstraction
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
