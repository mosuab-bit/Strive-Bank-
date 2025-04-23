using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Models.DTO
{
    public class Login_Request
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
