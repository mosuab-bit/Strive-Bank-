using BankSystem.API.Shared;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Request_UpdateEmployeeInfoByAdminDto
    {
        [Required(ErrorMessage = "User role is required.")]
        public UserRole UserRole { get; set; }  

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public int? Salary { get; set; }        

        public Branches? Branch { get; set; }   

        public bool IsDeleted { get; set; }
    }
}
