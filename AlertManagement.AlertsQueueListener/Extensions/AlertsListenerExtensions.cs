using AlertManagement.AlertsQueueListener.Listeners;
using Microsoft.Extensions.DependencyInjection;

namespace AlertManagement.AlertsQueueListener.Extensions
{
    public static class AlertsListenerExtensions
    {
        public static IServiceCollection AddAlertsQueueListener(this IServiceCollection services)
        {
            services.AddHostedService<AlertUpdateListener>();
            return services;
        }
    }
}