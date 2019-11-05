namespace EmailService
{
    public interface IEmailService
    {
        IEmailService Message();

        IEmailService From(string name, string address);

        IEmailService FromServer();

        IEmailService To(string name, string address);

        IEmailService WithSubject(string subject);

        IEmailService WithBodyPlain(string plainBody);

        IEmailService WithBodyHtml(string emailBody);

        bool Send();
    }
}