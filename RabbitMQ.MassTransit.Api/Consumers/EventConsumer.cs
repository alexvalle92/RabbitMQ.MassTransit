using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.MassTransit.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Consumers
{
    class EventConsumer :
        IConsumer<ICliente>
    {
        ILogger<EventConsumer> _logger;

        public EventConsumer(ILogger<EventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICliente> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.NomeCliente);
        }
    }
}
