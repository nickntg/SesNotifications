using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Mappings;
using SesNotifications.DataAccess.Repositories;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard
                    .ConnectionString(connectionString)
                    .Dialect<PostgreSQL82Dialect>())
                .Mappings(m => m.FluentMappings
                    .Add<SesDeliveryMap>()
                    .Add<SesComplaintMap>()
                    .Add<SesNotificationMap>()
                    .Add<SesBounceMap>()
                    .Add<SesOpenMap>())
                .BuildSessionFactory();

            SessionManager.Build(sessionFactory);

            return services;
        }

        public static IServiceCollection AddMvcWithUnitOfWork(this IServiceCollection services, int order)
        {
            services.AddMvc(x => { x.Filters.AddService<UnitOfWorkFilter>(order); });

            return services;
        }

        public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(UnitOfWorkFilter), typeof(UnitOfWorkFilter));
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<ISesBouncesRepository, SesBouncesRepository>();
            services.AddScoped<ISesDeliveriesRepository, SesDeliveriesRepository>();
            services.AddScoped<ISesComplaintsRepository, SesComplaintsRepository>();
            services.AddScoped<ISesOpensRepository, SesOpensRepository>();

            return services;
        }
    }
}
