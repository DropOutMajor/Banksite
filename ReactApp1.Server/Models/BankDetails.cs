using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models
{
    public class BankDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{8,20}$",
            ErrorMessage = "AccountNumber must be 8–20 digits.")]
        public string AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{6}[A-Z2-9][A-NP-Z0-9]([A-Z0-9]{3})?$",
            ErrorMessage = "SwiftCode must be a valid SWIFT format.")]
        public string SwiftCode { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public Payment Payment { get; set; }
    }
}