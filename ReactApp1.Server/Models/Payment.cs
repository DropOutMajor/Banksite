using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a 3‐letter uppercase code.")]
        public string Currency { get; set; }

        [Required]
        public string Bank { get; set; }

        // Timestamp when the payment was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // New: Has this payment been approved by an employee?
        public bool IsApproved { get; set; } = false;

        // When the approval occurred (null if still pending)
        public DateTime? ApprovedAt { get; set; }

      
    }
}
