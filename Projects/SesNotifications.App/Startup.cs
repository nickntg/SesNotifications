using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SesNotifications.App.Infrastructure;
using SesNotifications.App.Services;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess;

namespace SesNotifications.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddNHibernate(Configuration.GetConnectionString("Database"));

            services.AddScopedRepositories();
            ConfigureScopedRepositories(services);

            var oAuth = Configuration.GetSection("GoogleOAuth").Get<GoogleOAuth>();

            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = Constants.ApplicationScheme;
                    o.DefaultSignInScheme = Constants.SignInScheme;
                })
                .AddCookie(Constants.ApplicationScheme)
                .AddCookie(Constants.SignInScheme)
                .AddGoogle(o =>
                {
                    o.ClientId = oAuth.ClientId;
                    o.ClientSecret = oAuth.ClientSecret;
                });

            services.AddMvcWithUnitOfWork(0);   //Note: this won't work with Razor pages.

            services.AddRazorPages()
                .AddRazorPagesOptions(o =>
                {
                    o.Conventions.AuthorizeFolder("/");
                    o.Conventions.AllowAnonymousToPage("/Index");
                });
        }

        public void ConfigureScopedRepositories(IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ISearchService, SearchService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}