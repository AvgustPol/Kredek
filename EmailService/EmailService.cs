using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailService : IEmailBuilder
    {
        private readonly SmtpClient _smtpClient;
        private MimeMessage _message;

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public IEmailBuilder Message()
        {
            _message = new MimeMessage();
            return this;
        }

        public IEmailBuilder From(string name, string address)
        {
            _message.From.Add(new MailboxAddress(name, address));
            return this;
        }

        public IEmailBuilder To(string name, string address)
        {
            _message.To.Add(new MailboxAddress(name, address));
            return this;
        }

        public IEmailBuilder WithSubject(string subject)
        {
            _message.Subject = subject;
            return this;
        }

        public IEmailBuilder WithBodyPlain(string plainBody)
        {
            _message.Body = new TextPart("plain")
            {
                Text = plainBody
            };
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
    }
}