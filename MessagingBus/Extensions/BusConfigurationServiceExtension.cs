using System;
using System.Collections.Generic;
using ApplicationServices.Interfaces;
using MessagingBus.Publishers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MessagingBus.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMessagingBus(this IServiceCollection services, 
            IWebHostEnvironment env, IConfiguration config)
        {
            services.ConfigureBus(env, config);

            services.TryAddScoped<IMessagePublisher, MessagePublisher>();

            return services;
        }
        
        private static void ConfigureBus(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            var dictionary = new Dictionary<string, Func<IServiceCollection, IConfiguration, IServiceCollection>>
            {
                { Environments.Development, ConfigureRabbitMqOverMasstransit.ConfigureBus},
                { Environments.Staging, ConfigureRabbitMqOverMasstransit.ConfigureBus},
                { Environments.Production, ConfigureRabbitMqOverMasstransit.ConfigureBus}
            };

            dictionary[env.EnvironmentName](services, config);
        }
    }
}