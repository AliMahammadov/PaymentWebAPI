using PaymentWebEntity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebService.Services.Abstraction
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
