using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentWebEntity.DTOs;
using PaymentWebService.Services.Abstraction;
using PaymentWebService.Services.Concrete;

namespace PaymentWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceServices _balanceServices;

        public BalanceController(IBalanceServices balanceServices)
        {
            _balanceServices = balanceServices;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddBalance(int userId, decimal amount)
        {
            try
            {
                await _balanceServices.AddBalanceAsync(userId, amount);
                return Ok(new { Message = "Balance added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
