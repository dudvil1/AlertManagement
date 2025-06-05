using AlertManagement.FlightsQueueService.Implementations;
using AlertManagement.FlightsQueueService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlertManagement.FlightsQueueService.Extensions
{
    public static class FlightQueueExtensions
    {
        public static IServiceCollection AddFlightQueueService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFlightQueueService, RabbitFlightQueueService>();
            return services;
        }
    }
}
