using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/bankdetails")]
    [ApiController]
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.BankDetails.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bank details submitted successfully." });
        }
    }
}

