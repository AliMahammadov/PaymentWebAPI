using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentWebEntity.DTOs;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // [HttpGet("get-all")]
        // public IActionResult GetAllUsers()
        // {
        //     var users = _userService.GetAll();
        //     return Ok(users);
        // }
        
        [HttpGet("Details")]
        public async Task<ActionResult<UserDto>> GetUserDetails()
        {
            // Token-dən istifadəçi ID-sini tap
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub") ?? User.FindFirst("userId");

            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return Unauthorized("Token düzgün daxil edilməyib və ya istifadəçi ID-si tapılmadı.");

            // ID-ni int formatına çevirməyə çalış
            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Token içindəki istifadəçi ID-si düzgün formatda deyil.");

            // İstifadəçini servisdən al
            var userDetails = await _userService.GetUserByIdAsync(userId);

            if (userDetails == null)
                return NotFound("İstifadəçi mövcud deyil.");

            return Ok(userDetails);
        }

        
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
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

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto user)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Token etibarsızdır.");
            }

            await _userService.UpdateUserByIdAsync(int.Parse(userId), user);

            return Ok("İstifadəçi uğurla yeniləndi.");
        }


    }
}
