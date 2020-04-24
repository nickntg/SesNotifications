using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            services.AddMvcWithUnitOfWork(0);

            services.AddControllersWithViews();
        }

        public void ConfigureScopedRepositories(IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}