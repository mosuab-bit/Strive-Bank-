using BankSystem.API.Shared;
using System.Transactions;

namespace BankSystem.API.Models.Domain
{
    public class CustomerAccount
    {
        public int CustomerAccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public required string AccountNumber { get; set; }
        public bool IsDeleted { get; set; }

        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int AccountTypeId { get; set; }
        public  AccountType? AccountType { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
        public ICollection<CreditCard> CreditCards { get; set;} = new List<CreditCard>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection <LoanApplication> LoansApplication { get; set; } = new List <LoanApplication>();
       
    }
}
