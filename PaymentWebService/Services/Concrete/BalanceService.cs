using Microsoft.EntityFrameworkCore;
using PaymentWebData.DAL;
using PaymentWebEntity.DTOs;
using PaymentWebEntity.DTOs.BalancrDTOs;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebService.Services.Concrete
{
    public class BalanceService : IBalanceServices
    {
        private readonly AppDbContext _context;

        public BalanceService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddBalanceAsync(int userId, decimal amount)
        {
            if (amount <= 0) throw new InvalidOperationException("Meblegi duzgun daxil edin");
            var user = await _context.Users
                .Include(u => u.Balance)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new KeyNotFoundException("Istifadeci movcud deyil");
            if (user.Balance == null) throw new InvalidOperationException("Istifadeci Balansi movcud deyil");

            var payment = new Payment
            {
                Amount = amount,
                UserId = user.Id,
                CreateDate = DateTime.Now,
            };

            await _context.Payments.AddAsync(payment);

            user.Balance.TotalBalance += amount;
            user.Balance.AvailableBalance += amount;
            user.Balance.UpdateDate = DateTime.UtcNow;

            _context.Balances.Update(user.Balance);
            await _context.SaveChangesAsync();
        }

    }
}
