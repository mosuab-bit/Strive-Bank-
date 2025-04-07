namespace BankSystem.API.Models.Domain
{
    public class LoanType
    {
        public int LoanTypeId { get; set; }
        public required string LoanTypeName { get; set; }
        public double InterestRate { get; set; }
        public required string Description { get; set; }

        public int LoanTermMonths { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
    }
}
