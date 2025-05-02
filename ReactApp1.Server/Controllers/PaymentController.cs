using Microsoft.AspNetCore.Mvc;


namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        [Route("payment")]
        public IActionResult ProcessPayment([FromBody] PaymentDto payment)
        {
            if (payment.Amount <= 0 || string.IsNullOrEmpty(payment.Currency) || string.IsNullOrEmpty(payment.Bank))
                return BadRequest("Invalid payment details.");

            // Simulate saving to database or processing logic
            return Ok("Payment successful.");
        }

        public class PaymentDto
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public string Bank { get; set; }
        }
    }
}
