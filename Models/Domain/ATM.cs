namespace BankSystem.API.Models.Domain
{
    public class ATM
    {
        public int ATMId { get; set; }
        public required string Location { get; set; } = string.Empty;
        public required string Status { get; set; } = "Active";

        public int BranchId { get; set; }
        public required Branch Branch { get; set; }
    }
}
