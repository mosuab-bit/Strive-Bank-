using BankSystem.API.Data;
using BankSystem.API.Helper;
using BankSystem.API.Models.Domain;
using BankSystem.API.Models.DTO;
using BankSystem.API.Repositories.Interface;
using BankSystem.API.Shared;
using Humanizer;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankSystem.API.Repositories.Service
{
    public class CreditCardRepository (BankSystemDbContext context) : ICreditCard
    {
        public async Task CreateCreditCardAsync(Request_CreditCardDto request_CreditCard, string UserId)
        {
           
                var customerAccount = await context.CustomerAccounts.FindAsync(request_CreditCard.CustomerAccountId);
               
                if (customerAccount == null)
                throw new KeyNotFoundException("Customer account not found.");

            bool cardExists = await context.CreditCards.AnyAsync(c =>
                                        c.CustomerAccountId == request_CreditCard.CustomerAccountId &&
                                        c.CardType == request_CreditCard.CardType.ToString());

                if (cardExists)
                throw new InvalidOperationException("A credit card of this type already exists for this account.");

            var creditCard = new CreditCard
                {
                    CustomerAccountId = request_CreditCard.CustomerAccountId,
                    CardType = request_CreditCard.CardType.ToString(), 
                    CreditLimit = request_CreditCard.CreditLimit,
                    ExpiryDate = request_CreditCard.ExpiryDate,
                    Status = CreditCardStatus.Active.ToString(),
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    PinCode = EncryptionHelper.Encrypt(request_CreditCard.PinCode),
                    CreatedBy = UserId 
                };

                context.CreditCards.Add(creditCard);
                await context.SaveChangesAsync();
                  
        }

        public async Task DeleteCreditCardAsync(int CreditCardId)
        {
            var deleteCreditCard = await context.CreditCards.FirstOrDefaultAsync(c => c.CreditCardId == CreditCardId);

            if (deleteCreditCard == null)
                throw new KeyNotFoundException("Credit Card is not Found.");


            deleteCreditCard.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task<List<Response_GetCreditCardInfoDto>> GetAllCreditCardAsync(bool IncludeDeleted)
        {
            var Credits = context.CreditCards
                                .Where(c => IncludeDeleted || !c.IsDeleted)
                                .Include(c => c.CustomerAccount).ThenInclude(ca => ca.User);

            return await Credits.Select(c => new Response_GetCreditCardInfoDto
            {
                CreditCardId = c.CreditCardId,
                CardHolderName = c.CustomerAccount.User.FullName,
                CardType = c.CardType,
                CreditLimit = c.CreditLimit,
                Balance = c.CustomerAccount.Balance,
                ExpiryDate = c.ExpiryDate,
                Status = c.Status,
                CreatedAt = c.CreatedAt
            }).ToListAsync();
        }

        public async Task<Response_GetCreditCardInfoDto?> GetCreditCardByIdAsync(int CreditCardId)
        {
            var creditCard = await context.CreditCards
           .Where(c=> c.CreditCardId==CreditCardId && !c.IsDeleted)
           .Include(u => u.CustomerAccount)
           .ThenInclude(u => u.User)
           .Select(c => new Response_GetCreditCardInfoDto
           {
               CreditCardId = c.CreditCardId,
               CardHolderName = c.CustomerAccount.User.FullName,
               CardType = c.CardType,
               CreditLimit = c.CreditLimit,
               Balance = c.CustomerAccount.Balance,
               ExpiryDate = c.ExpiryDate,
               Status = c.Status,
               CreatedAt = c.CreatedAt
           })
           .FirstOrDefaultAsync();

            if(creditCard == null) return null;

            return creditCard;
            
        }

        public async Task UpdateCreditCardAsync(int CreditCardId,string userId, Request_UserUpdateCreditCardDto request_UserUpdateCreditCard)
        {
            var card = await context.CreditCards
                                  .Include(c => c.CustomerAccount)
                                  .ThenInclude(a => a.User)
                                  .FirstOrDefaultAsync(c => c.CreditCardId == CreditCardId && !c.IsDeleted);

            if (card == null)
                throw new KeyNotFoundException("Credit card not found.");

            if (card.CustomerAccount.User.Id != userId)
                throw new UnauthorizedAccessException("You are not allowed to update this card.");

            card.PinCode = request_UserUpdateCreditCard.PinCode;

            await context.SaveChangesAsync();
           
        }

        public async Task UpdateCreditCardByAdminAsync(int CreditCardId, Request_UpdateCreditCardByAdminDto request_UserUpdateCreditCardByAdmin)
        {
            var card = await context.CreditCards.FirstOrDefaultAsync(c => c.CreditCardId == CreditCardId);

            if (card == null) throw new KeyNotFoundException("Credit card not found.");

            card.CardType = request_UserUpdateCreditCardByAdmin.CardType.ToString();
            card.CreditLimit = request_UserUpdateCreditCardByAdmin.CreditLimit;
            card.ExpiryDate = request_UserUpdateCreditCardByAdmin.ExpiryDate;
            card.Status = request_UserUpdateCreditCardByAdmin.Status.ToString();
            card.IsDeleted = request_UserUpdateCreditCardByAdmin.IsDeleted;
           
            await context.SaveChangesAsync();
        }
    }
    
}
