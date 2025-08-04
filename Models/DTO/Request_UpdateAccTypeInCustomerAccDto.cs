using BankSystem.API.Shared;

namespace BankSystem.API.Models.DTO
{
    public class Request_UpdateAccTypeInCustomerAccDto
    {
        // public int customerAccountID { get; set; }
        public AccountType AccountTypeName { get; set; }
        public string OldAccountNumber { get; set; }
        public string NewAccountNumber { get; set; }
        public string ConfirmNewAccountNumber { get; set; }
    }
}
