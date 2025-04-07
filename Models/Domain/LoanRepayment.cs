namespace BankSystem.API.Models.Domain
{
    public class LoanRepayment
    {
        public int LoanRepaymentId { get; set; }

        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal RemainingBalance { get; set; }

        public int LoanId { get; set; }
        public required Loan Loan { get; set; }
    }
}
