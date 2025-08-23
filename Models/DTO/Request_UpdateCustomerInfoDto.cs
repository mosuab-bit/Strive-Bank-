using BankSystem.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Request_UpdateCustomerInfoDto
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address can't exceed 100 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [ValidationImage(".png,.jpg ,.jpeg")]
        [DataType(DataType.Upload)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username should be between 5 and 20 characters.")]
        public string UserName { get; set; }
    }
}
