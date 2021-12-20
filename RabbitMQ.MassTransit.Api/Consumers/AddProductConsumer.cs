using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.MassTransit.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Consumers
{
    public class AddProductConsumer : IConsumer<ProductModel>
    {
        ILogger<AddProductConsumer> _logger;
        public AddProductConsumer(ILogger<AddProductConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ProductModel> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message);
        }
    }
}
