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

    public async Task<bool> TransferBalanceAsync(int senderId, string receiverPhoneNumber, decimal amount)
    {
        var sender = await _context.Users.Include(u=>u.Balance).FirstOrDefaultAsync(u=>u.Id == senderId);
        if (sender == null || amount<0 || amount== 0) throw new InvalidOperationException("Meblegi duzgun daxil edin");
        if (amount > sender.Balance.AvailableBalance)  throw new InvalidOperationException("Balansinizda kifayet qeder vesait yoxdur");
           
        var receiver = await _context.Users.Include(u=>u.Balance).FirstOrDefaultAsync(u=> u.PhoneNumber == receiverPhoneNumber);
        if (receiver == null) throw new KeyNotFoundException("Bu nomrede istifadeci movcud deyil");
       
        var payment = new Payment
        {
            Amount = amount,
            UserId = sender.Id, 
            User = sender,
            CreateDate = DateTime.Now,
        };
        await _context.Payments.AddAsync(payment);
       
        sender.Balance.TotalBalance -= amount;
        sender.Balance.AvailableBalance -= amount;
        
        receiver.Balance.TotalBalance += amount;
        receiver.Balance.AvailableBalance += amount;
        
        await _context.SaveChangesAsync();
        return true;
    }
}