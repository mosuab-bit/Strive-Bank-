using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<Response_EmployeeDto> GetEmployeeInfoByUserNameAsync(string userName);
        Task<bool> UpdateEmployeeInfoByUserNameAsync(string username, Request_UpdateEmployeeInfoDto Dto);
        Task<List<Response_EmployeeDto>> GetAllEmployeeInfoByUserNameAsync(bool IncludedDeleted);
        Task<bool> DeleteEmployeeAsyunc(string userId);
        Task<bool> UpdateEmployeeByEmployeeId(int employeeId, Request_UpdateEmployeeInfoByAdminDto dto);
    }
}
