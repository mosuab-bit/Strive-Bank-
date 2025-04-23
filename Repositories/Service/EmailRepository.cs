﻿using BankSystem.API.Repositories.Interface;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Newtonsoft.Json.Linq;

namespace BankSystem.API.Repositories.Service
{
    public class EmailRepository : IEmail
    {
        private readonly string _apiKey = "20e0b14a2473ad105ddd312d9140f74e";
        private readonly string _apiSecret = "6d0e299336d383bc201a3e1782f39708";
        public readonly MailjetClient mailjet;
        private readonly IConfiguration _configuration;
        public EmailRepository(MailjetClient mailjet, IConfiguration configuration)
        {
            this.mailjet = mailjet;
            _configuration = configuration;

        }
        public async Task SendRegistrationEmailAsync(string recipientEmail, string userName, string AccountNum)
        {

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("musabibrahimflaifel@gmail.com", "Mus'ab Bank"))
                .WithSubject("Welcome to Our Bank!")
                .WithHtmlPart($"<h3>Hello {userName},</h3><p>Thank you for registering with us. We're excited to have you on board!<br>" +
                $"Now you are have an account bank with account number ==> {AccountNum}</p>")
                .WithTo(new SendContact(recipientEmail))
                .Build();

            var response = await mailjet.SendTransactionalEmailAsync(email);

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
            new JObject { {"Email", recipientEmail} }
                });

                // Initialize the MailjetClient
                var mailjet = new MailjetClient("20e0b14a2473ad105ddd312d9140f74e", "6d0e299336d383bc201a3e1782f39708");

                // Send the email
                var response = await mailjet.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Email sent successfully
                }
                else
                {
                    Console.WriteLine($"Failed to send email. Status: {response.StatusCode}, Error: {response.GetErrorInfo()}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while sending email: {ex.Message}");
                return false;
            }
        }
    }
}
