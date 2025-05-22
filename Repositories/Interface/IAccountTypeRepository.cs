using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface IAccountTypeRepository
    {
        public Task<List<Response_AccountTypeDto>> GetAccountTypesAsync();
        public Task<Response_AccountTypeDto?> GetAccountTypeByIdAsync(int accountTypeId);

        public Task<Response_AccountTypeDto> CreateAccountTypeAsync(Request_AccountTypeDto accountTypeDto);
        public Task<Response_AccountTypeDto?> UpdateAccountTypeAsync(int AccountTypeId,Request_AccountTypeDto request_AccountTypeDto);
        public Task<bool> DeleteAccountTypeAsync(int AccountTypeId);

    }
}
