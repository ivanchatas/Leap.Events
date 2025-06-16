using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Persistence.Helpers;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddRepositories();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton(factory => NHibernateSessionFactory.Create(connectionString));
            services.AddScoped(factory =>
            {
                var sessionFactory = factory.GetRequiredService<ISessionFactory>();
                return sessionFactory.OpenSession();
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>))
                .AddTransient<IEventRepository, EventRepository>()
                .AddTransient<ITicketSaleRepository, TicketSaleRepository>();
        }
    }
}
