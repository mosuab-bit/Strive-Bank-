namespace BankSystem.API.Models.DTO
{
    public class Response_RegistrationDto
    {
        public string? UserName { get; set; }
        public string? AccountNmber { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string confirmLink { get; set; }
    }
}
