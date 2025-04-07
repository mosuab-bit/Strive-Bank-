namespace BankSystem.API.Models.Domain
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public required string AccountTypeName { get; set; }
        public required string Description { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();
    }
}
