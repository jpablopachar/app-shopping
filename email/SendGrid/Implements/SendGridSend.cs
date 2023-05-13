using email.SendGrid.Interfaces;
using email.SendGrid.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace email.SendGrid.Implements
{
    public class SendGridSend : ISendGridSend
    {
        public async Task<(bool result, string? errorMessage)> SendEmail(SendGridData data)
        {
            try {
                var sendGridClient = new SendGridClient(data.SendGridApiKey);
                var to = new EmailAddress(data.EmailTo, data.NameTo);
                var emailTitle = data.Title;
                var from = new EmailAddress("jpablopachar1993@gmail.com", "Juan Pablo Pachar");
                var content = data.Content;
                var message = MailHelper.CreateSingleEmail(from, to, emailTitle, content, content);

                await sendGridClient.SendEmailAsync(message);

                return (true, null);
            } catch (Exception ex) {
                return (false, ex.Message);
            }
        }
    }
}