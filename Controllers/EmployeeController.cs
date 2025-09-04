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
             var isUpdate = await employeeRepository.UpdateEmployeeInfoByUserNameAsync(Username, dto);

            return Ok("Employee Updated Successfully");
        }

        [Authorize(Roles = "Admin,BranchManager")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee([FromQuery] bool IncludeDeleted = true)
        {
            var employees = await employeeRepository.GetAllEmployeeAsync(IncludeDeleted);
            
            if (employees == null || !employees.Any())
                return NotFound("No employees found.");

            return Ok(employees);
        }

        [Authorize(Roles = "Admin,BranchManager")]
        [HttpPut("Admin/{EmployeeId}")]
        public async Task<IActionResult> UpdateEmployeeById(int EmployeeId,[FromBody] Request_UpdateEmployeeInfoByAdminDto dto)
        {
            var isUpdated = await employeeRepository.UpdateEmployeeByEmployeeIdAsync(EmployeeId, dto);
            return Ok("Employee Updated Successfully");
        }

        [Authorize(Roles = "Admin,BranchManager")]
        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(string UserId)
        {
            var isDeleted = await employeeRepository.DeleteEmployeeAsync(UserId);
            return Ok("Employee Deleted Successfully");
        }
    }
}
