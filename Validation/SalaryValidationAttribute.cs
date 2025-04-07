using BankSystem.API.Models.DTO;
using BankSystem.API.Shared;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Validation
{
    public class SalaryValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var objectInstance = validationContext.ObjectInstance as RegisterRequestDto;

            if (objectInstance == null)
            {
                return new ValidationResult("Invalid object type.");
            }

            if (UserRole.Customer != objectInstance.UserRole
                && (value == null || !((int?)value).HasValue))
            {
                return new ValidationResult($"Salary is required for {objectInstance.UserRole.ToString()}.");
            }


            return ValidationResult.Success;
        }
    }
}
