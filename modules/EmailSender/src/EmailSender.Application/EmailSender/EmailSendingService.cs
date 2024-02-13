using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace EmailSender.EmailSender
{
    public class EmailSendingService 
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSendingService> _logger;

        public EmailSendingService(IConfiguration configuration, ILogger<EmailSendingService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(/*string toEmail, string subject, string content*/)
        {
            //var apiKey = _configuration["App:SendGridApiKey"];
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("noreply@example.com", "Your Application Name");
            //var to = new EmailAddress(toEmail);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            //var response = await client.SendEmailAsync(msg);
            _logger.LogInformation("Email sent");


        }
    }
}
