using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/bankdetails")]
    [ApiController]
    [Authorize]  // ← only authenticated users can submit bank details
    public class BankDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostBankDetails([FromBody] BankDetails model)
        {
            // 1) Server‐side model validation (including RegEx on AccountNumber/SwiftCode)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2) Verify that the PaymentId exists and is approved before allowing bank details
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == model.PaymentId);

            if (payment == null)
                return NotFound(new { message = "Payment not found." });

            if (!payment.IsApproved)
                return BadRequest(new { message = "Cannot submit bank details for an unapproved payment." });

            // 3) All checks passed—save bank details
            _context.BankDetails.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bank details submitted successfully." });
        }
    }
}
