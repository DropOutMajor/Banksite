namespace ReactApp1.Server.Models
{
    public class Payment
    {
        public int Id { get; set; }  // Primary key
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Bank { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
