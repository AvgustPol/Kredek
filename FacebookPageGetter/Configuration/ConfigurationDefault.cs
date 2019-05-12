using AutoMapper;
using FacebookPageGetter.Models;
using FacebookPageGetter.Models.Profiles;
using FacebookPageGetter.Services.FacebookClient;
using FacebookPageGetter.Services.FacebookService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FacebookPageGetter.Configuration
{
    public class ConfigurationDefault
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(new FacebookSettings()
            {
                AccessToken = configuration.GetSection("FacebookSettings").GetSection("AccessToken").Value,
                Id = configuration.GetSection("FacebookSettings").GetSection("Id").Value,
                Secret = configuration.GetSection("FacebookSettings").GetSection("Secret").Value
            });
            serviceCollection.AddSingleton<IFacebookClient, FacebookClient>();
            serviceCollection.AddSingleton<IFacebookService, FacebookService>();

            // AutoMapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FeedProfile());
            });
            serviceCollection.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}