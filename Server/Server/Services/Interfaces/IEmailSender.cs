using SendGrid;

namespace Server.Services.Interfaces
{
    public interface IEmailSender
    {
        Task<Response> SendEmail(User user, string subject, string content);
    }
}