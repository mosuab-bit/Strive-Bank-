using BankSystem.API.Models.DTO;
using BankSystem.API.Shared;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Validation
{
    public class BranchValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var objectInstance = validationContext.ObjectInstance as RegisterRequestDto;

            if (objectInstance == null)
            {
                return new ValidationResult("Invalid object type.");
            }

            if (UserRole.Customer != objectInstance.UserRole
                && (value == null || !((Branches?)value).HasValue))
            {
                return new ValidationResult($"Branch name is required for {objectInstance.UserRole.ToString()}.");
            }


            return ValidationResult.Success;
        }
    }
}
