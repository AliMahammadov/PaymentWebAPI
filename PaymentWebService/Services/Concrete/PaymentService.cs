using Microsoft.EntityFrameworkCore;
using PaymentWebData.DAL;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebService.Services.Concrete;

public class PaymentService : IPaymetServices
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> TransferBalanceAsync(string senderPhoneNumber, string receiverPhoneNumber, decimal amount)
    {
        var sender = await _context.Users
            .Include(u=>u.Balance)
            .FirstOrDefaultAsync(u => u.PhoneNumber == senderPhoneNumber);
            
        var receiver = await _context.Users
            .Include(u => u.Balance)
            .FirstOrDefaultAsync(u => u.PhoneNumber == receiverPhoneNumber);
        var payment = new Payment
        {
            Amount = amount,
            UserId = sender.Id, 
            User = sender
        };
        await _context.Payments.AddAsync(payment);
        if (sender == null || receiver == null || sender.Balance.AvailableBalance < amount)
        {
            return false;
        }
        sender.Balance.TotalBalance -= amount;
        sender.Balance.AvailableBalance -= amount;
        
        receiver.Balance.TotalBalance += amount;
        receiver.Balance.AvailableBalance += amount;
        
        await _context.SaveChangesAsync();
        return true;
    }
}