using BankSystem.API.Models.DTO;
using System.Security.Claims;

namespace BankSystem.API.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<Response_RegistrationDto> RegisterUserAsync(RegisterRequestDto registerDto);
       
    }
}
