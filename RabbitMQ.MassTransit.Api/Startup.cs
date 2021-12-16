using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.MassTransit.Api.Consumers;
using RabbitMQ.MassTransit.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RabbitMQ MassTransit API",
                    Description = "API básica para exemplo masstransit rabbitmq",
                    Contact = new OpenApiContact
                    {
                        Name = "Alexandre Valle",
                        Email = "alex.rvalle1@gmail.com"
                    }
                });
            });

            services.AddMassTransit(bus =>
            {
                bus.AddConsumer<EventConsumer>();

                bus.UsingRabbitMq((ctx, busConfigurator) =>
                {
                    busConfigurator.Host(Configuration.GetConnectionString("RabbitMq"));

                    busConfigurator.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<EventConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
