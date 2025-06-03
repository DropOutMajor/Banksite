namespace ReactApp1.Server.Models
{
    public class BankDetails
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string AccountNumber { get; set; }
        public string SwiftCode { get; set; }

        public Payment Payment { get; set; }
    }
}