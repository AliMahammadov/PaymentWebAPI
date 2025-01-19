using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentWebEntity.DTOs;
using PaymentWebService.Services.Abstraction;
using PaymentWebService.Services.Concrete;

namespace PaymentWebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class PaymentController : ControllerBase
    //{
    //    private readonly IPaymentService _paymentService;

    //    public PaymentController(IPaymentService paymentService)
    //    {
    //        _paymentService = paymentService;
    //    }
    //    [HttpGet("Id")]
    //    public async Task<IActionResult> GetUserByIdAsync(int id)
    //    {

    //        var user = await _paymentService.GetPaymentByIdAsync(id);
    //        return Ok(user);
    //    }
    //    [HttpPost("Get")]
    //    public async Task<IActionResult> AddPaymentAsync([FromBody] PaymentCreateDto payment)
    //    {
    //        await _paymentService.AddPaymentAsync(payment);
    //        return Ok(payment);
    //    }
    //}
}
