using Microsoft.AspNetCore.Mvc;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymetServices _paymentService;

        public PaymentController(IPaymetServices paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferBalance(string senderPhoneNumber, string receiverPhoneNumber, decimal amount)
        {
            var result = await _paymentService.TransferBalanceAsync(senderPhoneNumber, receiverPhoneNumber, amount);
            if (!result)
                return BadRequest("Balansinizda kifayet qeder .");

            return Ok("Kocurulme ugurla tamamlandi");
        }
    }
}