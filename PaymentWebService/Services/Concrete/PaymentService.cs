using PaymentWebData.DAL;
using PaymentWebEntity.DTOs;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebService.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Payment> GetPaymentByIdAsync(int? id)
        {
            if (!id.HasValue)
                throw new ArgumentNullException(nameof(id)); // ID boşdursa, səhv atırıq

            var payment = await _context.Payments.FindAsync(id.Value); // Verilən ID ilə ödənişi tapırıq
            if (payment == null)
                throw new KeyNotFoundException("Payment not found."); // Əgər tapılmasa, səhv atırıq

            return payment; // Tapılan ödənişi geri qaytarırıq
        }
        public async Task AddPaymentAsync(PaymentCreateDto paymentDto)
        {
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                //UserId = paymentDto.UserId,
            };
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

    public Task DeletePaymentAsync(int? id)
        {
            throw new NotImplementedException();
        }

       

        public Task UpdatePaymentByIdAsync(int? id, Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
