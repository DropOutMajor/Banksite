using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // ✅ 1. POST: Process a new payment (already exists)
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(new { id = payment.Id });
        }

        // ✅ 2. GET: Fetch all payments
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }

        // ✅ 3. POST: Approve a payment
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApprovePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            payment.IsApproved = true;
            payment.ApprovedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        // ✅ 4. POST: Reject (delete) a payment
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
