using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface IBranch
    {
        public Task<List<Response_BranchDto>> GetAllBranchesAsync();
        public Task<Response_BranchDto?> GetBranchById(int id);
        public Task<Response_BranchDto?> CreateBranchAsync(Request_BranchDto branch);
        public Task<Response_BranchDto?> UpdateBranchAsync(int id, Request_BranchDto branch);
        public Task<bool> DeleteBranchAsync(int id);
    }
}
