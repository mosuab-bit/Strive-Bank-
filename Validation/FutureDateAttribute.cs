using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Validation
{
    public class FutureDateAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be in the future.");
                }
            }
            return ValidationResult.Success; 
        }
    }
}
