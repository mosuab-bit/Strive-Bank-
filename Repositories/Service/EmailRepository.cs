using BankSystem.API.Repositories.Interface;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client;
using Newtonsoft.Json.Linq;

public class EmailRepository : IEmail
{
    private readonly MailjetClient _mailjet;
    private readonly IConfiguration _configuration;

    public EmailRepository(MailjetClient mailjet, IConfiguration configuration)
    {
        _mailjet = mailjet;
        _configuration = configuration;
    }

    public async Task SendRegistrationEmailAsync(string recipientEmail, string userName, string accountNum)
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact("musabibrahimflaifel@gmail.com", "Mus'ab Bank"))
            .WithSubject("Welcome to Our Bank!")
            .WithHtmlPart($"<h3>Hello {userName},</h3><p>Thank you for registering!<br>Your account number is: <strong>{accountNum}</strong></p>")
            .WithTo(new SendContact(recipientEmail))
            .Build();

        var response = await _mailjet.SendTransactionalEmailAsync(email);

        if (response.Messages[0].Status != "success")
        {
            throw new Exception("Failed to send registration email.");
        }
    }

    public async Task<bool> SendEmailAsync(string recipientEmail, string participantName, string subject, string htmlPart)
    {
        try
        {
            var request = new MailjetRequest
            {
                Resource = Mailjet.Client.Resources.Send.Resource,
            }
            .Property(Mailjet.Client.Resources.Send.FromEmail, "musabibrahimflaifel@gmail.com")
            .Property(Mailjet.Client.Resources.Send.FromName, "Events")
            .Property(Mailjet.Client.Resources.Send.Subject, subject)
            .Property(Mailjet.Client.Resources.Send.HtmlPart, htmlPart)
            .Property(Mailjet.Client.Resources.Send.Recipients, new JArray
            {
                new JObject { { "Email", recipientEmail } }
            });

            var response = await _mailjet.PostAsync(request);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while sending email: {ex.Message}");
            return false;
        }
    }
}
