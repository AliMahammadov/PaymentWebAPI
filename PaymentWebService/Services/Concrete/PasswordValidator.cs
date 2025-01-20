using Microsoft.AspNetCore.Identity;
using PaymentWebService.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentWebService.Services.Concrete
{
    public class PasswordValidator: IPasswordValidator
    {
        public bool Validate(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
