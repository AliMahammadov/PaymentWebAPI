using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymetServices _paymentService;

        public PaymentController(IPaymetServices paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> TransferBalance(string receiverPhoneNumber, decimal amount)
        {
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized("Yanlis token");
            
            int senderId = int.Parse(userIdClaim);
            var result = await _paymentService.TransferBalanceAsync(senderId, receiverPhoneNumber, amount);
            if (!result)
                return BadRequest("Balansinizda kifayet qeder vesait yoxdur .");

            return Ok("Kocurulme ugurla tamamlandi");
        }
    }
}