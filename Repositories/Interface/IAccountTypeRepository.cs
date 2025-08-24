using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface IAccountTypeRepository
    {
         Task<List<Response_AccountTypeDto>> GetAccountTypesAsync();
         Task<Response_AccountTypeDto?> GetAccountTypeByIdAsync(int accountTypeId);

         Task<Response_AccountTypeDto> CreateAccountTypeAsync(Request_AccountTypeDto accountTypeDto);
         Task<Response_AccountTypeDto?> UpdateAccountTypeAsync(int AccountTypeId,Request_AccountTypeDto request_AccountTypeDto);
         Task<bool> DeleteAccountTypeAsync(int AccountTypeId);

    }
}
