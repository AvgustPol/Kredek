using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService
{
    public class ConfigurationDefault
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            SmtpClient client = new SmtpClient();

            string address = configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Address").Value;
            int port = int.Parse(configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Port").Value);
            bool useSsl = bool.Parse(configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("UseSsl").Value);
            client.Connect(address, port, useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None);

            string username = configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Username").Value;
            string password = configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Password").Value;
            client.Authenticate(username, password);

            string serverName = configuration.GetSection("EmailSystem").GetSection("ServerName").Value;

            serviceCollection.AddSingleton(client);

            serviceCollection.AddTransient<IEmailService, EmailService>(provider => new EmailService(client, serverName, username));
        }
    }
}