using BankSystem.API.Data;
using BankSystem.API.Helper;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories.Service
{
    public class CustomerAccountRepository(BankSystemDbContext context) : ICustomerInterface
    {
        public async Task<Response_CustomerAccount> GetAccountInfoByAccountNum(string AccountNum)
        {
           var CustomerAccount = await context.CustomerAccounts
                .Include(ca => ca.User)
                .Include(ca => ca.AccountType)
                .FirstOrDefaultAsync(ca => ca.AccountNumber == AccountNum);

            if (CustomerAccount == null)
                throw new KeyNotFoundException("This Account Number Is Not Exist");
            
            var AccountInfo = new Response_CustomerAccount
            {
                FullName = customerAccounts.User.FullName,
                Email = customerAccounts.User.Email,
                Phone = customerAccounts.User.PhoneNumber,
                Address = customerAccounts.User.Address,
                UserRole = customerAccounts.User.Role.ToString(),
                IsDeleted = customerAccounts.User.IsDeleted,
                Gender = customerAccounts.User.Gender,
                AccountTypeName = customerAccounts.AccountType.AccountTypeName,
                CreatedDate = customerAccounts.CreatedDate,
                AccountNumber = Base64Helper.Decode(customerAccounts.AccountNumber),
                Balance = customerAccounts.Balance,
                PersonalImage = customerAccounts.User.PersonalImage
            } 
        }

        public async Task<Response_UpdateAccTypeInCustomerAccDto> GetAccountTypeByCustomerAccountIdAsync(int CustomerAccountId)
        {
            var CustomerAccount = await context.CustomerAccounts
                 .Include(ca => ca.User)
                 .Include(ca => ca.AccountType)
                 .FirstOrDefaultAsync(acc => acc.CustomerAccountId == CustomerAccountId);
            
            if (CustomerAccount == null ) 
                throw new KeyNotFoundException($"No Coustomer Account With ID : {CustomerAccountId}");

            if (CustomerAccount.User == null)
                throw new InvalidOperationException("User Information Is Not Available For This Account");
            if (CustomerAccount.AccountType == null)
            {
                throw new InvalidOperationException("AccountType information is not available for this customer account.");
            }

            return new Response_UpdateAccTypeInCustomerAccDto
            {
                FullName = CustomerAccount.User.FullName,
                AccountTypeName = CustomerAccount.AccountType.AccountTypeName
            };
        }

        public async Task<List<Response_CustomerAccount>> GetCustomersAccountsInfoAsync(bool IncludeDeleted = false)
        {
            var customerAccounts = await context.CustomerAccounts
         .Include(ca => ca.User)
         .Include(ca => ca.AccountType)
         .Where((ca => IncludeDeleted || !ca.User.IsDeleted))
         .Select(ca => new Response_CustomerAccount
         {
             FullName = ca.User.FullName,
             Email = ca.User.Email,
             Phone = ca.User.PhoneNumber,
             Address = ca.User.Address,
             UserRole = ca.User.Role.ToString(),
             IsDeleted = ca.User.IsDeleted,
             Gender = ca.User.Gender,
             AccountTypeName = ca.AccountType.AccountTypeName,
             PersonalImage = ca.User.PersonalImage,
             CreatedDate = ca.CreatedDate,
         })
         .ToListAsync();

            return customerAccounts;
        }

        public async Task<Response_UpdateAccTypeInCustomerAccDto> UpdateAccountInfoAsync(Request_UpdateAccTypeInCustomerAccDto request_UpdateAccTypeInCustomerAccDto,int CustomerAccountId)
        {
            var customerAccount = await context.CustomerAccounts
                .Include(ca => ca.User)
                .Include(ca => ca.AccountType)
                .FirstOrDefaultAsync(acc => acc.CustomerAccountId == CustomerAccountId);

            if (customerAccount == null)
            {
                throw new KeyNotFoundException($"No customer account found with ID: {customerAccount}");
            }

            if (customerAccount.User == null)
            {
                throw new InvalidOperationException("User information is not available for this customer account.");
            }

            if (customerAccount.AccountType == null)
            {
                throw new InvalidOperationException("AccountType information is not available for this customer account.");
            }


            customerAccount.AccountTypeId = (int)request_UpdateAccTypeInCustomerAccDto.AccountTypeName;
            var decryptedAccountNumber = EncryptionHelper.Decrypt(customerAccount.AccountNumber);

            if (decryptedAccountNumber == request_UpdateAccTypeInCustomerAccDto.OldAccountNumber)
            {

                if (request_UpdateAccTypeInCustomerAccDto.NewAccountNumber == request_UpdateAccTypeInCustomerAccDto.ConfirmNewAccountNumber)
                {
                    var newAccountNumber = EncryptionHelper.Encrypt(request_UpdateAccTypeInCustomerAccDto.NewAccountNumber);
                    customerAccount.AccountNumber = newAccountNumber;
                }
                else
                {
                    throw new InvalidOperationException("The new account number and confirmation do not match.");
                }
            }
            else
            {
                throw new InvalidOperationException("The provided old account number does not match the current account number.");
            }
            await context.SaveChangesAsync();

            string last4Digits = request_UpdateAccTypeInCustomerAccDto.NewAccountNumber.Length >= 4
            ? request_UpdateAccTypeInCustomerAccDto.NewAccountNumber.Substring(request_UpdateAccTypeInCustomerAccDto.NewAccountNumber.Length - 4)
            : request_UpdateAccTypeInCustomerAccDto.NewAccountNumber;

            return new Response_UpdateAccTypeInCustomerAccDto
            {
                FullName = customerAccount.User.FullName,
                AccountTypeName = request_UpdateAccTypeInCustomerAccDto.AccountTypeName.ToString(),
                AccountNumber = $"New account number is **** **** {last4Digits}",
                Message = "Account type has been successfully updated."
            };
        }


    }
}
