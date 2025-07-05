using BankSystem.API.Models.DTO;

namespace BankSystem.API.Repositories.Interface
{
    public interface ICreditCard
    {
        Task CreateCreditCardAsync(Request_CreditCardDto request_CreditCard, string UserId);
        Task UpdateCreditCardAsync(int CreditCardId,string userId, Request_UserUpdateCreditCardDto request_UserUpdateCreditCard);
        Task UpdateCreditCardByAdminAsync(int CreditCardId,Request_UpdateCreditCardByAdminDto request_UserUpdateCreditCardByAdmin);
        Task<List<Response_GetCreditCardInfoDto>> GetAllCreditCardAsync(bool IncludeDeleted);
        Task<Response_GetCreditCardInfoDto?> GetCreditCardByIdAsync(int CreditCardId);
        Task DeleteCreditCardAsync(int CreditCardId);
    }
}
