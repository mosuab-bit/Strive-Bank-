namespace BankSystem.API.Models.Domain
{
    public class Branch
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public required string BranchLocation { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<ATM> ATMs { get; set; } = new List<ATM>();
    }
}
