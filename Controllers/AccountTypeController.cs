using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController(IAccountTypeRepository accountType) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAccountTypes()
        {
            var result = await accountType.GetAccountTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,CreditCardOfficer")]
        public async Task<IActionResult> GetAccountTypeById(int id)
        {
            var accountTypeIsExist = await accountType.GetAccountTypeByIdAsync(id);

            if(accountTypeIsExist == null) return NotFound();

            return Ok(accountTypeIsExist);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAccountType([FromBody] Request_AccountTypeDto request_AccountTypeDto)
        {
            var newAccountType = await accountType.CreateAccountTypeAsync(request_AccountTypeDto);
            
            return CreatedAtAction(nameof(GetAccountTypeById), new {newAccountType.AccountTypeId}, newAccountType);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,CreditCardOfficer")]
        public async Task<IActionResult> UpdateAccountType(int id,[FromBody]Request_AccountTypeDto request_AccountTypeDto)
        {
            var updateAccountType = await accountType.UpdateAccountTypeAsync(id, request_AccountTypeDto);
            
            if(updateAccountType == null) return NotFound();

            return Ok(updateAccountType);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,CreditCardOfficer")]
        public async Task<IActionResult> DeleteAccountType(int id)
        {
            var deleteAccountType = await accountType.DeleteAccountTypeAsync(id);

            if(!deleteAccountType) return NotFound();

            return NoContent();
        }


    }

}
