using BankSystem.API.Shared;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Request_UserUpdateCreditCardDto
    {
        

        [Required(ErrorMessage = "PIN Code is required.")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "PIN Code must be a numeric value consisting of 4 to 6 digits.")]
        public string PinCode { get; set; }
    }
}
