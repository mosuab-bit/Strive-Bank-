using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController (ICreditCard creditCardService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "SystemAdministrator,CreditCardOfficer,Teller")]
        public async Task<IActionResult> CreateCreditCard(Request_CreditCardDto creditCardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await creditCardService.CreateCreditCardAsync(creditCardDto, userId);

            return Ok("Credit card created successfully.");
        }

        [HttpPatch("Customer/{CreditCardId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCreditCardById(int CreditCardId, [FromBody] Request_UserUpdateCreditCardDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await creditCardService.UpdateCreditCardAsync(CreditCardId, userId, dto);

            return NoContent(); 
        }

        [HttpPatch("Admin/{CreditCardId}")]
        [Authorize(Roles = "Admin,Teller,CreditCardOfficer")]
        public async Task<IActionResult> UpdateCreditCardByAdmin(int CreditCardId, [FromBody] Request_UpdateCreditCardByAdminDto updateCreditCardByAdminDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await creditCardService.UpdateCreditCardByAdminAsync(CreditCardId, updateCreditCardByAdminDto);

            return NoContent();

        }

        [HttpGet]
        [Authorize(Roles = "SystemAdministrator,CreditCardOfficer,Teller")]
        public async Task<IActionResult> GetAllCreditCards([FromQuery] bool IncludeDeleted = false)
        {
            var cards = await creditCardService.GetAllCreditCardAsync(IncludeDeleted);

            return Ok(cards);
        }

        [HttpGet("{CreditCardId}")]
        [Authorize(Roles = "SystemAdministrator,CreditCardOfficer,Teller,Customer")]
        public async Task<IActionResult> GetCreditCardById(int CreditCardId)
        {
            var creditCard = await creditCardService.GetCreditCardByIdAsync(CreditCardId);

            if (creditCard is null)
                return NotFound($"Credit card with ID {CreditCardId} was not found.");

            return Ok(creditCard);
        }

    }
}
