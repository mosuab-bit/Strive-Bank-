using BankSystem.API.Shared;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Request_UpdateCreditCardByAdminDto
    {
        [Required]
        public CreditCardType CardType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Credit Limit must be a positive value.")]
        public double CreditLimit { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public CreditCardStatus Status { get; set; }

        public bool IsDeleted { get; set; }
       
    }
}
