using EmailService.Factories;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _serverSenderName;
        private readonly string _serverSenderEmail;

        private readonly string _serverReceiverEmail;
        private readonly string _serverReceiverName;
        
        private readonly ISmtpClientFactory _smtpClientFactory;

        private MimeMessage _message;
        private SmtpClient _smtpClient;

        public EmailService(ISmtpClientFactory smtpClientFactory, string serverSenderName, string serverSenderEmail, string serverReceiverEmail, string serverReceiverName)
        {
            _smtpClientFactory = smtpClientFactory;

            _serverSenderName = serverSenderName;
            _serverSenderEmail = serverSenderEmail;

            _serverReceiverEmail = serverReceiverEmail;
            _serverReceiverName = serverReceiverName;
        }

        public IEmailService From(string name, string address)
        {
            _message.From.Add(new MailboxAddress(name, address));
            return this;
        }

        public IEmailService FromServer()
        {
            _message.From.Add(new MailboxAddress(_serverSenderName, _serverSenderEmail));
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
            _message.To.Add(new MailboxAddress(_serverReceiverName, _serverReceiverEmail));
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