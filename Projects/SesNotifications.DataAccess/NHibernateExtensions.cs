using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using SesNotifications.DataAccess.Mappings;

namespace SesNotifications.DataAccess
{
    public static class NHibernateExtensions
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
                    .Add<SesBounceMap>())
                .BuildSessionFactory();

            SessionManager.Build(sessionFactory);

            return services;
        }

        public static IServiceCollection AddMvcWithUnitOfWork(this IServiceCollection services)
        {
            services.AddMvc(x => { x.Filters.AddService<UnitOfWorkFilter>(1); });

            return services;
        }
    }
}
