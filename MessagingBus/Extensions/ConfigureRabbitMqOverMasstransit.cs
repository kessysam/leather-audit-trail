using System;
using GreenPipes;
using MassTransit;
using MessagingBus.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessagingBus.Extensions
{
    internal static class ConfigureRabbitMqOverMasstransit
    {
        internal static IServiceCollection ConfigureBus(this IServiceCollection services, IConfiguration config)
        {
            var username = config["RabbitMQ:Username"];
            var password = config["RabbitMQ:Password"];
            var rabbitMqUrl = config["RabbitMQ:Url"]; 

            services.AddMassTransit(x =>
            {
                //register the consumers
                x.AddConsumer<AuditTrailConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(rabbitMqUrl), h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint("AuditTrailQueue", e =>
                    {
                        e.Durable = true;
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(3, 2000));
                        e.UseCircuitBreaker(cb =>
                        {
                            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                            cb.TripThreshold = 15;
                            cb.ActiveThreshold = 10;
                            cb.ResetInterval = TimeSpan.FromMinutes(3);
                        });
                        e.Consumer<AuditTrailConsumer>(provider);
                        e.DiscardSkippedMessages();
                        e.DiscardFaultedMessages();
                    });
                }));
            });

            services.AddSingleton<IHostedService, MessagingBusService>();
            return services;
        }
    }
}