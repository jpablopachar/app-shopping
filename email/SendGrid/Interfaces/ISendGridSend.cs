using email.SendGrid.Models;

namespace email.SendGrid.Interfaces
{
    public interface ISendGridSend
    {
        Task<(bool result, string? errorMessage)> SendEmail(SendGridData data);
    }
}