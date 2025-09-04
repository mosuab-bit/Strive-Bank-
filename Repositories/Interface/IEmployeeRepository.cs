using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<Response_EmployeeDto> GetEmployeeInfoByUserNameAsync(string userName);
        Task<bool> UpdateEmployeeInfoByUserNameAsync(string username, Request_UpdateEmployeeInfoDto Dto);
        Task<List<Response_EmployeeDto>> GetAllEmployeeAsync(bool IncludedDeleted);
        Task<bool> DeleteEmployeeAsync(string userId);
        Task<bool> UpdateEmployeeByEmployeeIdAsync(int employeeId, Request_UpdateEmployeeInfoByAdminDto dto);
    }
}
