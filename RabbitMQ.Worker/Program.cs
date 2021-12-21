using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Worker.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddHostedService<Worker>();
                    services.AddMassTransit(bus =>
                    {
                        bus.AddConsumer<AddCustomerConsumer>();
                        bus.AddConsumer<SendMailConsumer>();
                        bus.AddConsumer<AddProductConsumer>();

                        bus.UsingRabbitMq((ctx, busConfigurator) =>
                        {
                            busConfigurator.Host(configuration.GetConnectionString("RabbitMq"));

                            busConfigurator.ReceiveEndpoint("addProductQueue", e =>
                            {
                                e.PrefetchCount = 10;
                                e.UseMessageRetry(r => r.Interval(2, 1000));
                                e.ConfigureConsumer<AddProductConsumer>(ctx);
                            });

                            busConfigurator.ReceiveEndpoint("sendMailQueue", e =>
                            {
                                e.ConfigureConsumer<SendMailConsumer>(ctx);
                            });
                        });
                    });
                    services.AddMassTransitHostedService();


                });
    }
}
