using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService
{
    public class ConfigurationDefault
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var client = new SmtpClient();

            string address = configuration.GetSection("SmtpClient").GetSection("Address").Value;
            int port = int.Parse(configuration.GetSection("SmtpClient").GetSection("Port").Value);
            string username = configuration.GetSection("SmtpClient").GetSection("Username").Value;
            string password = configuration.GetSection("SmtpClient").GetSection("Password").Value;
            bool useSsl = bool.Parse(configuration.GetSection("SmtpClient").GetSection("UseSsl").Value);

            client.Connect(address, port, useSsl);
            client.Authenticate(username, password);

            serviceCollection.AddSingleton(client);
        }
    }
}