using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using BankSystem.API.Repositories.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<ApplicationUser> userManager,JWTServices jWTServices,IAccountRepository accountRepository) : ControllerBase
    {
        [HttpPost("Login")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 423)]
        public async Task<IActionResult> Login([FromForm] Login_Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =await accountRepository.LoginAsync(request);
            if (result.Success)
                return Ok(new
                {
                    accessToken = result.AccessToken,
                    refreshToken = result.RefreshToken
                });

            if (result.Message.Contains("locked", StringComparison.OrdinalIgnoreCase))
                return StatusCode(423, result.Message);

            return BadRequest(result.Message);


        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await accountRepository.RegisterUserAsync(registerRequest);

            if (result.UserName == null)
            {
                return BadRequest(result.Message);
            }

            return Ok(new
            {
                message = "Registration successful! , please check your email to Confirm Email ",
                result.UserName,
                result.Success,
            });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Invalid token or user ID.");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found.");

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return BadRequest("Email confirmation failed.");

            // ✅ بعد تأكيد البريد الإلكتروني، يتم توليد التوكن
            var accessToken = jWTServices.GenerateJwtToken(user);

            return Ok(new
            {
                message = "Email confirmed successfully.",
                accessToken = accessToken
            });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await accountRepository.Logout(User);
            Response.Cookies.Delete("AuthToken");
            return Ok(new {Message = "Successfully logged out"});
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordReqDto forgotPasswordDto)
        {
            var result = await accountRepository.ForgotPasswordAsync(forgotPasswordDto);
            if (!result)
            {
                return BadRequest("Failed to send reset email.");
            }

            return Ok("Password reset link sent.");
        }

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest(new { Message = "Passwords do not match" });
            }

            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            // Reset the password using the resetToken
            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }

            return Ok(new { Message = "Password has been successfully reset." });
        }
    }
}
