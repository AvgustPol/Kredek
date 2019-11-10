using EmailService.Factories;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _serverEmail;
        private readonly string _serverName;
        private readonly ISmtpClientFactory _smtpClientFactory;

        private MimeMessage _message;
        private SmtpClient _smtpClient;

        public EmailService(ISmtpClientFactory smtpClientFactory, string serverName, string serverEmail)
        {
            _smtpClientFactory = smtpClientFactory;
            _serverName = serverName;
            _serverEmail = serverEmail;
        }

        public IEmailService From(string name, string address)
        {
            _message.From.Add(new MailboxAddress(name, address));
            return this;
        }

        public IEmailService FromServer()
        {
            _message.From.Add(new MailboxAddress(_serverName, _serverEmail));
            return this;
        }

        public IEmailService Message()
        {
            _smtpClient = _smtpClientFactory.GetSmtpClient();
            _message = new MimeMessage();
            return this;
        }

        public bool Send()
        {
            bool isSent = true;

            _smtpClient.Send(_message);
            _smtpClient.Disconnect(true);
            _smtpClient.Dispose();

            return isSent;
        }

        public IEmailService To(string name, string address)
        {
            _message.To.Add(new MailboxAddress(name, address));
            return this;
        }

        public IEmailService ToServer()
        {
            _message.To.Add(new MailboxAddress(_serverName, _serverEmail));
            return this;
        }

        public IEmailService WithBodyHtml(string emailBody)
        {
            _message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBody
            };
            return this;
        }

        public IEmailService WithBodyPlain(string plainBody)
        {
            _message.Body = new TextPart("plain")
            {
                Text = plainBody
            };
            return this;
        }

        public IEmailService WithSubject(string subject)
        {
            _message.Subject = subject;
            return this;
        }
    }
}