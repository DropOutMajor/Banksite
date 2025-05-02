using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models; // Assuming the Payment model is inside the Models folder

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("paymentform")] // Changed the route to 'paymentform' as per the frontend request
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment)
        {
            if (payment.Amount <= 0 || string.IsNullOrEmpty(payment.Currency) || string.IsNullOrEmpty(payment.Bank))
                return BadRequest("Invalid payment details.");

            // Add payment to the database
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Return a success message after saving
            return Ok("Payment successful.");
        }
    }
}
