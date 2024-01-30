using Infrastructure;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MassTransit;

namespace WebApp.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection SetupDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>();

        return services;
    }

    public static IServiceCollection SetupRepositories(this IServiceCollection services)
    {
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();

        return services;
    }

    public static IServiceCollection SetupServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ISpecializationService, SpecializationService>();

        return services;
    }

    public static IServiceCollection SetupMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host(new Uri(configuration["RabbitMq:Host"]!), hostConfigurator =>
                {
                    hostConfigurator.Username(configuration["RabbitMq:Username"]);
                    hostConfigurator.Password(configuration["RabbitMq:Password"]);
                });
                
                factoryConfigurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
