using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("paymentform")]
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
            // 1) Server‐side validation using Data Annotations
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2) At this point, Amount > 0, Currency matches /^[A-Z]{3}$/, etc., per model attributes

            // 3) Save to database
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // 4) Return the newly created Id for downstream use (e.g., linking BankDetails)
            return Ok(new { id = payment.Id });
        }
    }
}
