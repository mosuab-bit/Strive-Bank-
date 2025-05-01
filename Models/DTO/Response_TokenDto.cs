namespace BankSystem.API.Models.DTO
{
    public class Response_TokenDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Message { get; set; }
        public bool  Success { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
