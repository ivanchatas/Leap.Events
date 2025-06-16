using System.Reflection;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IEventService, EventService>()
                .AddTransient<ITicketSaleService, TicketSaleService>();
        }
    }
}
