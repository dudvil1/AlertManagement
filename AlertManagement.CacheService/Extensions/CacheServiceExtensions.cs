using AlertManagement.CacheService.Configurations;
using AlertManagement.CacheService.Implementations;
using AlertManagement.CacheService.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;

public static class CacheServiceExtensions
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = new RedisOptions();
        configuration.GetSection("Redis").Bind(redisOptions);

        var redis = ConnectionMultiplexer.Connect(redisOptions.ConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(redis);
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
