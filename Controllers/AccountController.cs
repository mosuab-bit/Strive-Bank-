using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            var accessToken = _jwtService.GenerateJwtToken(user);

            return Ok(new
            {
                message = "Email confirmed successfully. You can now log in.",
                accessToken = accessToken
            });
        }
    }
}
