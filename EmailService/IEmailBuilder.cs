namespace EmailService
{
    public interface IEmailService
    {
        IEmailService From(string name, string address);

        IEmailService FromServer();

        IEmailService Message();

        bool Send();

        IEmailService To(string name, string address);

        IEmailService ToServer();

        IEmailService WithBodyHtml(string emailBody);

        IEmailService WithBodyPlain(string plainBody);

        IEmailService WithSubject(string subject);
    }
}