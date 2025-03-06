using Microsoft.EntityFrameworkCore;
using PaymentWebData.DAL;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;
using PaymentWebEntity.DTOs;
using PaymentWebEntity.DTOs.LoginRegister;
using System.Text.RegularExpressions;
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

            var existingUser = await _context.Users.FindAsync(id.Value);
            if (existingUser == null)
                throw new KeyNotFoundException("Istifadeci movcud deyil");

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && user.PhoneNumber != "string")
            {
                if (user.PhoneNumber.Length != 10 || !Regex.IsMatch(user.PhoneNumber, @"^\d+$"))
                    throw new InvalidOperationException("Telefon nömrəsini düzgün qeyd edin (Nümunə: 0706280899)");

                if (await _context.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber))
                    throw new InvalidOperationException("Bu nomre artiq istifade olunub.");

                existingUser.PhoneNumber = user.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && user.Email != "string")
            {
                if (!user.Email.Contains("@"))
                    throw new InvalidOperationException("Email ünvanı '@' simvolu daxil olmalıdır!");

                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                    throw new InvalidOperationException("Bu Email artiq istifade olunub.");

                existingUser.Email = user.Email;
            }

            if (!string.IsNullOrWhiteSpace(user.Password) && user.Password != "string")
            {
                if (user.Password != user.RepeatPassword)
                    throw new InvalidOperationException("Parolu düzgün daxil edin!");

                if (user.Password.Length < 5 || user.Password.Length > 8)
                    throw new InvalidOperationException("Parolun uzunluğu 5-8 simvol arasında olmalıdır!");

                if (!user.Password.Any(char.IsUpper))
                    throw new InvalidOperationException("Parol ən az bir böyük hərf içərməlidir!");

                if (!user.Password.Any(char.IsLower))
                    throw new InvalidOperationException("Parol ən az bir kiçik hərf içərməlidir!");

                existingUser.Password = user.Password;
                existingUser.RepeatPassword = user.RepeatPassword;
            }

            if (!string.IsNullOrWhiteSpace(user.FullName) && user.FullName != "string")
            {
                if (user.FullName.Length < 3 || user.FullName.Length > 20)
                    throw new InvalidOperationException("Adın uzunluğu 3-20 simvol arasında olmalıdır!");

                if (user.FullName.Any(char.IsDigit))
                    throw new InvalidOperationException("Ad daxilində rəqəm olmamalıdır!");

                var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
                existingUser.FullName = textInfo.ToTitleCase(user.FullName.ToLower());
            }

            existingUser.UpdateDate = DateTime.UtcNow;

            _context.Users.Update(existingUser);
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
            if (await _context.Users.AnyAsync(u => u.PhoneNumber == registerDto.PhoneNumber))
                throw new InvalidOperationException("Bu nomre artiq istifade olunub.");

            if (registerDto.PhoneNumber.Length != 10 || !Regex.IsMatch(registerDto.PhoneNumber, @"^\d+$"))
                throw new InvalidOperationException("Telefon nömrəsini düzgün qeyd edin (Nümunə: 0706280899)");
            if (!(registerDto.Password == registerDto.RepeatPassword))

                throw new InvalidOperationException("Parolu duzgun daxil edin!");
            if (!(registerDto.Password == registerDto.RepeatPassword))
                throw new InvalidOperationException("Parolu düzgün daxil edin!");

            if (registerDto.Password.Length < 5 || registerDto.Password.Length > 8)
                throw new InvalidOperationException("Parolun uzunluğu 5-8 simvol arasında olmalıdır!");

            if (!registerDto.Password.Any(char.IsUpper))
                throw new InvalidOperationException("Parol ən az bir böyük hərf içərməlidir!");

            if (!registerDto.Password.Any(char.IsLower))
                throw new InvalidOperationException("Parol ən az bir kiçik hərf içərməlidir!");

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
                PhoneNumber = registerDto.PhoneNumber,
                Password = registerDto.Password,
                RepeatPassword = registerDto.RepeatPassword,
                BalanceId = balance.Id,
                CreateDate = DateTime.UtcNow
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber);
            if (user == null)
                throw new KeyNotFoundException("Istifadeci movcud deyil.");
            if (user.Password != loginDto.Password)
                throw new KeyNotFoundException("Parolu duzgun daxil edin.");
        
            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
