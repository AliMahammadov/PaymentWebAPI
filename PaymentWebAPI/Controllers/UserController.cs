using Microsoft.AspNetCore.Mvc;
using PaymentWebEntity.DTOs;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("get-all")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails(int id)
        {
            var userDetails = await _userService.GetUserByIdAsync(id);

            if (userDetails == null)
            {
                return NotFound("User not found");
            }

            return Ok(userDetails);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDto user)
        {
                await _userService.AddUserAsync(user);
                return Ok(user);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(id);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUserById(int id, [FromBody] UserUpdateDto user)
        {
            await _userService.UpdateUserByIdAsync(id, user);

            return Ok(id);

        }

    }
}
