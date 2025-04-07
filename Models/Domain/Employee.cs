namespace BankSystem.API.Models.Domain
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public DateTime HireDate { get; set; }
        public int? EmployeeSalary { get; set; }
        public bool IsDeleted { get; set; }

        public required string UserId { get; set; }
        public  ApplicationUser? User { get; set; }

        public int BranchId { get; set; }
        public Branch? BranchEmployee { get; set; }

    }
}
