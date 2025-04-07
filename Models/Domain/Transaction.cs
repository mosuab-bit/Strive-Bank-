namespace BankSystem.API.Models.Domain
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        
        public required string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? BankFeeId { get; set; }

        public int CustomerAccountId { get; set; }
        public required CustomerAccount CustomerAccount { get; set; }
    }
}
