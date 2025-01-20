using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebService.Services.Abstraction
{
    public interface IPasswordValidator
    {
        bool Validate(string password);
    }
}
