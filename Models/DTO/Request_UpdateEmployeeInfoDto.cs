using BankSystem.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Request_UpdateEmployeeInfoDto
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(10, ErrorMessage = "It should contain of 10 numbers")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [ValidationImage(".png,.jpg ,.jpeg")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username should be between 5 and 20 characters.")]
        public string UserName { get; set; }
    }
}
