namespace BankSystem.API.Models.Domain
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        
        public required string CardType { get; set; }
        public double CreditLimit { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime ExpiryDate { get; set; }
        public required string Status { get; set; }
        public bool IsDeleted { get; set; }
        public required string PinCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CustomerAccountId { get; set; }
        public required CustomerAccount CustomerAccount { get; set; }
    }
}
