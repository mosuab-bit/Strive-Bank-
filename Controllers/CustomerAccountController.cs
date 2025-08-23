using Azure.Core;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController(ICustomerInterface customer) : ControllerBase
    {
        [Authorize(Roles = "Admin , BranchManager")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerAccount(bool includeDeleted = false)
        {
            var customerAccounts = await customer.GetCustomersAccountsInfoAsync(includeDeleted);

            if (customerAccounts == null || !customerAccounts.Any())
                return NotFound("No customer accounts found.");

            return Ok(customerAccounts);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{CustomerAccountId}")]
        public async Task<IActionResult> UpdateAccountInfo([FromBody] Request_UpdateAccTypeInCustomerAccDto request_UpdateAccType,int CustomerAccountId)
        {
            var result = await customer.UpdateAccountInfoAsync(request_UpdateAccType, CustomerAccountId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin,BranchManager,Customer")]
        [HttpGet("ById/{CustomerAccountId}")]
        public async Task<IActionResult> GetAccountTypeNameByCustomerAcountId(int CustomerAccountId)
        { 
            var AccountTypeName = await customer.GetAccountTypeByCustomerAccountIdAsync(CustomerAccountId);
            return Ok(AccountTypeName);
        }

        [Authorize(Roles = "Admin,BranchManager,Customer")]
        [HttpGet("ByAccountNumber/{AccountNumber}")]
        public async Task<IActionResult> GetAccountInfoByAccountNum(string AccountNumber)
        {
            var AccountInfo = await customer.GetAccountInfoByAccountNum(AccountNumber);

            return Ok(AccountInfo);
        }

        [Authorize(Roles = "Admin,BranchManager,Customer")]
        [HttpPatch("Update/{CustomerId}")]
        public async Task<IActionResult> UpdateCustomerInfo([FromForm]Request_UpdateCustomerInfoDto dto,int CustomerId)
        {
            bool isUpdated = await customer.UpdateCustomerInfoAsync(dto, CustomerId);
            return NoContent();
        }

        [Authorize(Roles = "Admin,BranchManager")]
        [HttpPatch("Delete/{CustomerAccountId}")]
        public async Task<IActionResult> DeleteCustomerAccount(int CustomerAccountId)
        {
            await customer.DeleteCustomerAccountAsync(CustomerAccountId);
            return NoContent();
        }
    }
}
