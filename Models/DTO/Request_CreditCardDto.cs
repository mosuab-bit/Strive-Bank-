using BankSystem.API.Shared;
using BankSystem.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Request_CreditCardDto
    {
        [Required(ErrorMessage = "Customer AccountId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer AccountId must be a positive integer.")]
        public int CustomerAccountId { get; set; }

        [Required(ErrorMessage = "Card Type is required.")]
        [EnumDataType(typeof(CreditCardType), ErrorMessage = "Invalid Card Type. Valid types are: Visa, MasterCard, AmericanExpress, Discover.")]
        public CreditCardType CardType { get; set; }

        [Required(ErrorMessage = "Credit Limit is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Credit Limit must be greater than 0.")]
        public double CreditLimit { get; set; }

        [Required(ErrorMessage = "ExpiryDate is required.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "ExpiryDate must be a future date.")]
        public DateTime ExpiryDate { get; set; }

        

        [Required(ErrorMessage = "PIN Code is required.")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "PIN Code must be a numeric value consisting of 4 to 6 digits.")]
        public string PinCode { get; set; } = string.Empty;

    }
}
