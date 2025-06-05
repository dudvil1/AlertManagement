using AlertManagement.AddAlert.AlertBusinessLogic.Interfaces;
using AlertManagement.AddAlert.AlertBusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AlertManagement.AddAlert.AlertController.Extensions
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IAlertService, AlertService>();
            return services;
        }
    }
}
