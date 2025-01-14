using PaymentWebEntity.DTOs;
using PaymentWebEntity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebService.Services.Abstraction
{
    public interface IPaymentService
    {
        Task AddPaymentAsync(PaymentCreateDto paymentDto);
        Task DeletePaymentAsync(int? id);
        Task<Payment> GetPaymentByIdAsync(int? id);
        Task UpdatePaymentByIdAsync(int? id, Payment payment);
    }
}
