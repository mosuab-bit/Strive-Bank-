using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface ICustomerInterface
    {
        Task<List<Response_CustomerAccount>> GetCustomersAccountsInfoAsync(bool IncludeDeleted);
        Task<Response_UpdateAccTypeInCustomerAccDto> UpdateAccountInfoAsync(Request_UpdateAccTypeInCustomerAccDto request_UpdateAccTypeInCustomerAccDto, int CustomerAccountId);
        Task<Response_UpdateAccTypeInCustomerAccDto> GetAccountTypeByCustomerAccountIdAsync(int CustomerAccountId);
        Task<Response_CustomerAccount> GetAccountInfoByAccountNum(string AccountNum);
        Task<bool> UpdateCustomerInfoAsync(Request_UpdateCustomerInfoDto request_UpdateCustomerInfoDto, int CustomerAccountId);
        Task<bool> DeleteCustomerAccountAsync(int CustomerAccountId);
    }
}
