using BankSystem.API.Models.DTO;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;

namespace BankSystem.API.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<Response_RegistrationDto> RegisterUserAsync(RegisterRequestDto registerDto);
        Task<Response_TokenDto> LoginAsync(Login_Request login_Request);
        Task Logout(ClaimsPrincipal claimsPrincipal);
        Task<bool> ForgotPasswordAsync(ForgotPasswordReqDto forgotPasswordReqDto);
    }
}
