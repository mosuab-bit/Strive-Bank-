using BankSystem.API.Data;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories.Service
{
    public class AccountTypeRepository(BankSystemDbContext dbContext) : IAccountTypeRepository
    {
        public async Task<List<Response_AccountTypeDto>> GetAccountTypesAsync()
        {
            return await dbContext.AccountTypes.Select(at=>
                  new Response_AccountTypeDto
                  {
                      AccountTypeId = at.AccountTypeId,
                      AccountTypeName = at.AccountTypeName,
                      Description = at.Description
                  })
                .ToListAsync();
        }

        public async Task<Response_AccountTypeDto?> GetAccountTypeByIdAsync(int accountTypeId)
        {
            var accountType = await dbContext.AccountTypes.FindAsync(accountTypeId);
            if(accountType==null)return null;

            return new Response_AccountTypeDto
            {

                AccountTypeId = accountType.AccountTypeId,
                AccountTypeName = accountType.AccountTypeName,
                Description = accountType.Description
            };
        }

        public async Task<Response_AccountTypeDto> CreateAccountTypeAsync(Request_AccountTypeDto accountTypeDto)
        {
            var newAccountType = new AccountType
            {
                AccountTypeName = accountTypeDto.AccountTypeName,
                Description = accountTypeDto.Description
            };

            dbContext.AccountTypes.Add(newAccountType);
            await dbContext.SaveChangesAsync();

            return new Response_AccountTypeDto
            {
                AccountTypeId = newAccountType.AccountTypeId,
                AccountTypeName = newAccountType.AccountTypeName,
                Description = newAccountType.Description
            };
        }

        public async Task<Response_AccountTypeDto?> UpdateAccountTypeAsync(int accountTypeId, Request_AccountTypeDto request_AccountTypeDto)
        {
            var accountType = await dbContext.AccountTypes.FindAsync(accountTypeId);

            if (accountType == null) return null;

            accountType.AccountTypeName = request_AccountTypeDto.AccountTypeName;
            accountType.Description = request_AccountTypeDto.Description;

            await dbContext.SaveChangesAsync();

            return new Response_AccountTypeDto
            {
                AccountTypeId = accountType.AccountTypeId,
                AccountTypeName = accountType.AccountTypeName,
                Description = accountType.Description
            };
        }

        public async Task<bool> DeleteAccountTypeAsync(int accountTypeId)
        {
            var accountType = await dbContext.AccountTypes.FindAsync(accountTypeId);

            if(accountType == null) return false;

            dbContext.AccountTypes.Remove(accountType);
            await dbContext.SaveChangesAsync();

            return true;
        }
        
    }
}
