using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class ForgotPasswordReqDto
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }   
    }
}
