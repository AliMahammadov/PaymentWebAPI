using PaymentWebEntity.DTOs;
using PaymentWebEntity.Entities;

namespace PaymentWebService.Services.Abstraction
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserByIdAsync(int? Id);

        IEnumerable<User> GetAll();
        Task AddUserAsync(UserCreateDto userDto);

        Task DeleteUserAsync(int? Id);
        
        Task UpdateUserByIdAsync(int? id,UserUpdateDto user);
    }
}
