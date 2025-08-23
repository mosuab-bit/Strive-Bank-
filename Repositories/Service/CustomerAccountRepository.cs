using Azure.Core;
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
        public async Task<bool> DeleteCustomerAccountAsync(int customerAccountID)
        {
            var customerAccount = await context.CustomerAccounts.Include(ca => ca.User)
                .FirstOrDefaultAsync(ca => ca.CustomerAccountId == customerAccountID);

            if (customerAccount == null)
            {
                throw new KeyNotFoundException($"Customer account not found for CustomerAccount ID: {customerAccountID}");
            }


            customerAccount.IsDeleted = true;

            var user = customerAccount.User;
            if (user != null)
            {
                user.IsDeleted = true;
            }

            await context.SaveChangesAsync();

            return true;
        }
        



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
                FullName = CustomerAccount.User.FullName,
                Email = CustomerAccount.User.Email,
                Phone = CustomerAccount.User.PhoneNumber,
                Address = CustomerAccount.User.Address,
                UserRole = CustomerAccount.User.Role.ToString(),
                IsDeleted = CustomerAccount.User.IsDeleted,
                Gender = CustomerAccount.User.Gender,
                AccountTypeName = CustomerAccount.AccountType.AccountTypeName,
                CreatedDate = CustomerAccount.CreatedDate,

                Balance = CustomerAccount.Balance,
                PersonalImage = CustomerAccount.User.PersonalImage
            };

            return AccountInfo;
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

        public async Task<bool> UpdateCustomerInfoAsync(Request_UpdateCustomerInfoDto request_UpdateCustomerInfoDto, int CustomerAccountId)
        {
            var customerAccount = await context.CustomerAccounts.Include(u => u.User)
                .FirstOrDefaultAsync(ca => ca.CustomerAccountId == CustomerAccountId);

            if (customerAccount == null)
                throw new KeyNotFoundException("Customer Account is Not Exist");
            if (customerAccount.User == null)
                throw new Exception("User not found for the customer account.");
            
            customerAccount.User.PhoneNumber = request_UpdateCustomerInfoDto.PhoneNumber;
            customerAccount.User.Email = request_UpdateCustomerInfoDto.Email;
            customerAccount.User.UserName = request_UpdateCustomerInfoDto.UserName;
            customerAccount.User.Address = request_UpdateCustomerInfoDto.Address;

            if (!string.IsNullOrEmpty(request_UpdateCustomerInfoDto.ImageUrl))
            customerAccount.User.PersonalImage = request_UpdateCustomerInfoDto.ImageUrl;
            

            await context.SaveChangesAsync();

            return true;
        }


    }
}
