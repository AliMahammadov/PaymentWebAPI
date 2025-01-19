using PaymentWebEntity.DTOs;
using PaymentWebEntity.DTOs.BalancrDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebService.Services.Abstraction
{
    public interface IBalanceServices
    {
        Task AddBalanceAsync(int userId, decimal amount);
    }
}
