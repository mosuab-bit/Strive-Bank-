namespace BankSystem.API.Models.Domain
{
    public class LoanApplication
    {
        public int LoanApplicationId { get; set; }
        public required string ApplicantName { get; set; }
        public required string Address { get; set; }
        public required string ContactNumber { get; set; }
        public required string Email { get; set; }
        public decimal Income { get; set; }
        public required string EmploymentStatus { get; set; }
        public required string BankAccountNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public int LoanTermMonths { get; set; }  
        public DateTime ApplicationDate { get; set; }

        public int CustomerAccountId { get; set; }
        public required CustomerAccount CustomerAccount { get; set; } 

        public int LoanTypeId { get; set; }
        public required LoanType LoanType { get; set; }

        public required string RepaymentSchedule { get; set; }
        public bool IsSecuredLoan { get; set; }
        public required string CollateralDescription { get; set; }
        public required string ApplicationStatus { get; set; }
        public required Loan Loan { get; set; }
    }
}
