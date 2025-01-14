using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebEntity.DTOs
{
    public class UserDetailsDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal TotalBalance { get; set; }
        public List<PaymentDto>? Payments { get; set; }
    }

    public class PaymentDto
    {
        public decimal? Amount { get; set; }
        public string Date => DateTime.Now.ToString("yyyy-MM-dd Clock HH:mm");


    }
}
