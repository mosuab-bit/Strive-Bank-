using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IBranch branch) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var allBranches = await branch.GetAllBranchesAsync();

            return Ok(allBranches);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,BranchManager")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branchById = await branch.GetBranchById(id);
            if (branchById == null) return NotFound();

            return Ok(branchById);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,BranchManager")]
        public async Task<IActionResult> CreateBranch([FromBody]Request_BranchDto request_BranchDto)
        {
            var newBranch = await branch.CreateBranchAsync(request_BranchDto);

            if (newBranch == null) return BadRequest("Branch name is already exist");

            return Ok("Branch created successfully");
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin,BranchManager")]
        public async Task<IActionResult> UpdateBranchById(int id, [FromBody]Request_BranchDto request)
        {
            if (branch == null)
                return BadRequest();
            

            var updatedBranch = await branch.UpdateBranchAsync(id, request);
            
            if (updatedBranch == null)
                return NotFound("Branch is not found!");

            return Ok("Updated done successfully");

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BranchManager")]
        public async Task<IActionResult> DeleteBranchById(int id)
        {
            bool isDeleted = await branch.DeleteBranchAsync(id);
            if(!isDeleted)
                return NotFound("Branch is not exist");

            return Ok("Branch deleted successfully");
        }

    }
}
