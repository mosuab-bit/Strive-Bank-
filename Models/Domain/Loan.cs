namespace BankSystem.API.Models.Domain
{
    public class Loan
    {
        public int LoanId { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Status { get; set; }
        public DateTime ApprovedDate { get; set; }

        public int CustomerAccountId { get; set; }
        public required CustomerAccount CustomerAccount { get; set; }

        public int LoanTypeId { get; set; }
        public required LoanType LoanType { get; set; }

        public ICollection<LoanRepayment> LoanRepayments { get; set; } = new List<LoanRepayment>();

        public int LoanApplicationId { get; set; }//FK
        public required LoanApplication LoanApplication { get; set; }
    }
}
