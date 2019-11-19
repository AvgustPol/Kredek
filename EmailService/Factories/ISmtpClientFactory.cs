using MailKit.Net.Smtp;

namespace EmailService.Factories
{
    public interface ISmtpClientFactory
    {
        SmtpClient GetSmtpClient();
    }
}