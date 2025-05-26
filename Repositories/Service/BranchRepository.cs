using BankSystem.API.Data;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories.Service
{
    public class BranchRepository(BankSystemDbContext dbContext) : IBranch
    {
       
        public async Task<List<Response_BranchDto>> GetAllBranchesAsync()
        {
            var branches = await dbContext.Branches
                .Select(b=> new Response_BranchDto
                {
                    BranchId = b.BranchId,
                    BranchName = b.BranchName,
                    BranchLocation = b.BranchLocation,
                }).ToListAsync();

            return branches;
        }

        public async Task<Response_BranchDto?> GetBranchById(int id)
        {
            var branch = await dbContext.Branches.FindAsync(id);
            if (branch == null) return null;

            return new Response_BranchDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                BranchLocation = branch.BranchLocation,
            };

        }

        public async Task<Response_BranchDto?> CreateBranchAsync(Response_BranchDto branch)
        {
            bool isExist = await dbContext.Branches.AnyAsync(b => b.BranchName == branch.BranchName);

            if (isExist)
                return null;
            var newBranch = new Branch
            {
                BranchName = branch.BranchName,
                BranchLocation = branch.BranchLocation,
            };

            dbContext.Branches.Add(newBranch);
            await dbContext.SaveChangesAsync();

            return new Response_BranchDto
            {
                BranchId = newBranch.BranchId,
                BranchName = newBranch.BranchName,
                BranchLocation = newBranch.BranchLocation,
            };
        }

    }
}
