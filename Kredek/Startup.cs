using B2CGraph;
using Kredek.Data;
using Kredek.Data.DatabaseSeeding;
using Kredek.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kredek
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            dbInitializer.SeedDatabase();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Cookies

            //Custom class to manage cookies
            services.AddScoped<ICookiesManager, CookiesManager>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #endregion Cookies

            #region Azure_B2C_AD B2CGraphClient

            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));

            services.AddSingleton<IB2CGraphClient>(new B2CGraphClient(
                Configuration.GetSection("B2CGraphClient").GetSection("ClientId").Value,
                Configuration.GetSection("B2CGraphClient").GetSection("ClientSecret").Value,
                Configuration.GetSection("B2CGraphClient").GetSection("Tenant").Value));

            #endregion Azure_B2C_AD B2CGraphClient

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureDatabase(services);

            //configure facebook page getter
            FacebookPageGetter.Configuration.ConfigurationDefault.Configure(services, Configuration);
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IKredekInitializer, KredekInitializer>();
            services.AddScoped<IPreviewInitializer, PreviewInitializer>();
            services.AddScoped<IImageSavingService, ImageSavingService>();

            ConnectToTheDatabase(DatabaseType.SQL, services);
        }

        private void ConnectToTheDatabase(DatabaseType databaseType, IServiceCollection services)
        {
            switch (databaseType)
            {
                case DatabaseType.SQL:
                    services.AddDbContext<ApplicationDbContext>(
                        options => options.UseSqlServer(Configuration.GetConnectionString("DevelopmentSQLConnection")));
                    break;
                case DatabaseType.PostgreSQL:
                    services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(
                        options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection")));
                    break;
            }
        }

        private enum DatabaseType
        {
            PostgreSQL,
            SQL
        }
    }
}