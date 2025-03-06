using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebEntity.DTOs.LoginRegister
{
    public class LoginDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }

    public class RegisterDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
}
