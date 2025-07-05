namespace BankSystem.API.Models.DTO
{
    public class Response_GetCreditCardInfoDto
    {
        public int CreditCardId { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public double CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
