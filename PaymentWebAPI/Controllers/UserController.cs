using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentWebEntity.DTOs;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebAPI.Controllers
{
    //[Authorize]
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
        [Authorize]
        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            // var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Token təmin edilməyib.");

            try
            {
                await _userService.DeleteAccountAsync(token);
                return Ok("Hesab uğurla silindi.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
        // [Authorize]
        // [HttpDelete("delete")]
        // public async Task<IActionResult> DeleteUserAsync()
        // {
        //     var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //     if (string.IsNullOrEmpty(userId)) 
        //         return Unauthorized("User not found");
        //
        //     await _userService.DeleteUserAsync(userId);
        //     return Ok("User deleted successfully");
        // }



        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUserById(int id, [FromBody] UserUpdateDto user)
        {
            await _userService.UpdateUserByIdAsync(id, user);

            return Ok(id);

        }

    }
}
