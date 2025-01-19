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
            var user = await _context.Users
                .Include(u => u.Balance)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (user.Balance == null)
                throw new InvalidOperationException("User does not have a balance");

            user.Balance.TotalBalance += amount;
            user.Balance.AvailableBalance += amount;
            user.Balance.UpdateDate = DateTime.UtcNow;

            _context.Balances.Update(user.Balance);
            await _context.SaveChangesAsync();
        }

        //public async Task IncreaseBalanceAsync(BalanceUpdateDto balanceUpdateDto)
        //{
        //    var user = await _userService.GetUserByIdAsync(balanceUpdateDto.UserId);
        //    if (user == null)
        //        throw new KeyNotFoundException("User not found.");
        //    if (user.Balance == null)
        //        throw new InvalidOperationException("User has no associated balance.");

        //    user.Balance.TotalBalance += balanceUpdateDto.Amount;

        //  var changes =  await _dbContext.SaveChangesAsync();

        //}
    }
}
