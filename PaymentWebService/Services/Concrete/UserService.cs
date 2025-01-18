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
               .Include(u => u.Balance)   
               .FirstOrDefaultAsync();

            if (user == null)
            {
                return null; 
            }

            var userDetails = new UserDetailsDto
            {
                FullName = user.FullName,
                Email = user.Email,
                CreateDate = user.CreateDate,
                UpdateDate = user.UpdateDate,
                TotalBalance = user.Balance.TotalBalance,
                Payments = user.Payments.Select(p => new PaymentDto
                {
                    Amount = p.Amount,
                }).ToList()

            };
            return userDetails;
        }

        public async Task AddUserAsync(UserCreateDto userDto)
        {
            Balance balance = new Balance()
            {
                TotalBalance = 0,
                AvailableBalance = 0,
                CreateDate = DateTime.UtcNow
                
            };

            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();

            User user = new User()
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                BalanceId = balance.Id,
                CreateDate = DateTime.UtcNow,
            };

            await _context.Users.AddAsync(user);
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

            User.FullName = user.FullName;
            User.Email = user.Email;
            User.UpdateDate = DateTime.UtcNow;

            _context.Users.Update(User);
            await _context.SaveChangesAsync();

        }

      public IEnumerable<UserDto> GetAll()
{
    var users = _context.Set<User>()
                  .Include(u => u.Balance)  // Balance əlaqəsini yüklə
                  .Include(u => u.Payments) // Payment əlaqəsini yüklə
                  .ToList();

    var formattedData = users.Select(user => new UserDto
    {
        CreateDate = user.CreateDate,
        UpdateDate = user.UpdateDate,
        FullName = user.FullName,
        Email = user.Email,
   
       
        Balance = new BalanceDto
        {
            TotalBalance = user.Balance.TotalBalance,
            AvailableBalance = user.Balance.AvailableBalance
        },
        Payments = user.Payments.Select(payment => new PaymentDto
        {
            Amount = payment.Amount,
        }).ToList()
    }).ToList();

    return formattedData;
}

    }
}
