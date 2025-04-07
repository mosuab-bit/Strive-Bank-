using Microsoft.AspNetCore.Identity;

namespace BankSystem.API.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public required string FullName { get; set; }
       
        public required string Address { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Role { get; set; }
        public string? PersonalImage { get; set; }
        public bool IsDeleted { get; set; }
        public required string Gender { get; set; }
        public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();
        public Employee Employee { get; set; } = null!;
    }
}
