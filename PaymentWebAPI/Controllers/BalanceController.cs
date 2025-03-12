using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentWebEntity.DTOs;
using PaymentWebService.Services.Abstraction;
using PaymentWebService.Services.Concrete;

namespace PaymentWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceServices _balanceServices;

        public BalanceController(IBalanceServices balanceServices)
        {
            _balanceServices = balanceServices;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddBalance(decimal amount)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? throw new UnauthorizedAccessException("Token dogru deyil"));
                
                await _balanceServices.AddBalanceAsync(userId, amount);
                return Ok(new { Message = "Balans ugurla artirildi !!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
