using Microsoft.EntityFrameworkCore;
using PaymentWebData.DAL;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;
using PaymentWebEntity.DTOs;
namespace PaymentWebService.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserDetailsDto> GetUserByIdAsync(int? Id)
        {
            var user = await _context.Users
               .Where(u => u.Id == Id)
               .Include(u => u.Balance)   // Balance ilə əlaqəni daxil et.
               .Include(u => u.Payments)  // Payments ilə əlaqəni daxil et
               .FirstOrDefaultAsync();

            if (user == null)
            {
                return null; 
            }

            // UserDetailsDto yaratmaq və məlumatları qaytarmaq
            var userDetails = new UserDetailsDto
            {
                FullName = user.FullName,
                Email = user.Email,
                TotalBalance = user.Balance.TotalBalance,
                Payments = user.Payments.Select(p => new PaymentDto
                {
                    Amount = p.Amount,
                }).ToList()
            };
            return userDetails;
            //if (!Id.HasValue) throw new ArgumentNullException(nameof(Id));
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
            //if (user == null) throw new KeyNotFoundException("User not Found");
            //return user;
        }

        public async Task AddUserAsync(UserCreateDto userDto)
        {
            // Yeni Balance obyekti yaratmaq
            Balance balance = new Balance()
            {
                TotalBalance = 0, // Default olaraq 0
                AvailableBalance = 0,
                
            };

            // Balance-i əlavə et
            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();  // SaveChanges burada çağırılmalı ki, balance.Id yaradılıb saxlanılsın.

            // Yeni User obyekti yaratmaq
            User user = new User()
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                BalanceId = balance.Id,
                Data = DateTime.UtcNow
            };

            // User-i əlavə et
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();  // User-in ID-si burada bazada yaradılır

            // Balance modelindəki əlaqəni yenidən saxlayırıq, indi UserId təyin edirik

            //User.UserId = user.Id;

            // Balance obyektini yenidən saxlayırıq
            await _context.SaveChangesAsync();
        }



        public async Task DeleteUserAsync(int? Id)
        {
            if (!Id.HasValue) throw new ArgumentNullException(nameof(Id));
            var user = await _context.Users.FindAsync(Id.Value);
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

       

        public async Task UpdateUserByIdAsync(int? id, UserUpdateDto user)
        {
            if (!id.HasValue)
                throw new ArgumentNullException(nameof(id));

            var User = await _context.Users.FindAsync(id.Value);
            if (User == null)
                throw new KeyNotFoundException("User not found");

            // Update user properties
            User.FullName = user.FullName;
            User.Email = user.Email;

            _context.Users.Update(User);
            await _context.SaveChangesAsync();

        }

        IEnumerable<User> IUserService.GetAll()
        {

            return _context.Users.ToList();
        }
    }
}
