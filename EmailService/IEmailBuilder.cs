namespace EmailService
{
    public interface IEmailService
    {
        IEmailService Message();

        IEmailService From(string name, string address);

        IEmailService To(string name, string address);

        IEmailService WithSubject(string subject);

        IEmailService WithBodyPlain(string plainBody);

        bool Send();
    }
}