using Microsoft.EntityFrameworkCore;
using PaymentWebData.DAL;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;
using PaymentWebEntity.DTOs;
using PaymentWebEntity.DTOs.LoginRegister;
namespace PaymentWebService.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UserService(AppDbContext context, TokenService tokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenService = tokenService;
        }

        public async Task<UserDto> GetUserByIdAsync(int? Id)
        {
            var user = await _context.Users
               .Where(u => u.Id == Id)
               .Include(u => u.Balance)
               .Include(u => u.Payments)
               .FirstOrDefaultAsync();

            if (user == null) return null;
            var userDetails = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                CreateDate = user.CreateDate,
                UpdateDate = user.UpdateDate,
                Balance = new BalanceDto
                {
                    TotalBalance = user.Balance.TotalBalance,
                    AvailableBalance = user.Balance.AvailableBalance,
                    
                },

                Payments = user.Payments.Select(p => new PaymentDto
                {
                    Amount = p.Amount,
                    CreateDate = p.CreateDate
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
                PhoneNumber = userDto.PhoneNumber,
                Email = userDto.Email,
                Password = userDto.Password,
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
        Id = user.Id,
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
            CreateDate = payment.CreateDate,
        }).ToList()
    }).ToList();

    return formattedData;
}

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                throw new InvalidOperationException("Email already exists.");

            var balance = new Balance
            {
                TotalBalance = 0,
                AvailableBalance = 0,
                CreateDate = DateTime.UtcNow
            };
            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();

            var user = new User
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Password = registerDto.Password,
                BalanceId = balance.Id,
                CreateDate = DateTime.UtcNow
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                Email = user.Email
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
                throw new KeyNotFoundException("User not found.");
            if (user.Password != loginDto.Password)
                throw new KeyNotFoundException("User not found.");
            // Şifrələmə yoxlama əlavə edilə bilər.

            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                Email = user.Email
            };
        }
    }
}
