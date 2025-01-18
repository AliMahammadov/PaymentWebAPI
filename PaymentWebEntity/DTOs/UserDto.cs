using PaymentWebCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebEntity.DTOs
{
    public class UserDto:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public BalanceDto Balance { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }

    public class BalanceDto
    {
        public decimal TotalBalance { get; set; }
        public decimal AvailableBalance { get; set; }
    }

    

}
