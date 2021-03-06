﻿using EmailService.Factories;
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
            string address = configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Address").Value;
            int port = int.Parse(configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Port").Value);
            bool useSsl = bool.Parse(configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("UseSsl").Value);

            //Server sender E-mail
            string username = configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Username").Value;
            string password = configuration.GetSection("EmailSystem").GetSection("SmtpClient").GetSection("Password").Value;

            string serverSenderName = configuration.GetSection("EmailSystem").GetSection("ServerSenderName").Value;
            string serverReceiverName = configuration.GetSection("EmailSystem").GetSection("ServerReceiverEmail").Value;
            string serverReceiverEmail = configuration.GetSection("EmailSystem").GetSection("ServerReceiverEmail").Value;

            serviceCollection.AddTransient<ISmtpClientFactory>(s => 
                new SmtpClientFactory(password, 
                    address, 
                    port, 
                    username, 
                    useSsl));

            serviceCollection.AddTransient<IEmailService>(s => 
                new EmailService(
                    (ISmtpClientFactory)s.GetService(typeof(ISmtpClientFactory)), 
                    serverSenderName, 
                    username,
                    serverReceiverEmail,
                    serverReceiverName
                    ));
        }

    }
}