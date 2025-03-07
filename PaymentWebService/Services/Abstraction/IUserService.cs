using PaymentWebEntity.DTOs;
using PaymentWebEntity.DTOs.LoginRegister;

namespace PaymentWebService.Services.Abstraction
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int? Id);

        IEnumerable<UserDto> GetAll();

        Task AddUserAsync(UserCreateDto userDto);

        // Task DeleteUserAsync(int? Id);
        Task DeleteAccountAsync(string token);

        
        Task UpdateUserByIdAsync(int? id,UserUpdateDto user);


        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}
