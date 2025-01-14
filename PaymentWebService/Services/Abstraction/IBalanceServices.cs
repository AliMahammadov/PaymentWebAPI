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
        Task CreateBalanceAsync(BalanceDTO balanceDto);

        Task<BalanceDTO> GetBalanceAsync(int userId);

        Task UpdateBalanceAsync(BalanceDTO balanceDto);

        Task DeleteBalanceAsync(int userId);
    }
}
