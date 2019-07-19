namespace EmailService
{
    public interface IEmailBuilder
    {
        IEmailBuilder Message();

        IEmailBuilder From(string name, string address);

        IEmailBuilder To(string name, string address);

        IEmailBuilder WithSubject(string subject);

        IEmailBuilder WithBodyPlain(string plainBody);

        bool Send();
    }
}