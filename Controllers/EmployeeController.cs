using BankSystem.API.Data;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeRepository employeeRepository) : ControllerBase
    {
        [Authorize(Roles = "Admin,BranchManager")]
        [HttpGet("{Username}")]
        public async Task<IActionResult> GetEmployeeInfoByUserUsername(string Username)
        {
            var employee = await employeeRepository.GetEmployeeInfoByUserNameAsync(Username);

            return Ok(employee);
        }

        [Authorize(Roles = "Admin,BranchManager,Teller,CreditCardOfficer,LoanOfficer")]
        [HttpPut("{Username}")]
        public async Task<IActionResult> UpdateEmployeeInfoByUsername(string Username, [FromForm] Request_UpdateEmployeeInfoDto dto)
        {
             
        }
    }
}
