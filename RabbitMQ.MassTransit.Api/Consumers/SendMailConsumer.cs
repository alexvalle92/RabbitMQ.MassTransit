using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.MassTransit.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Consumers
{
    public class SendMailConsumer : IConsumer<EmailModel>
    {
        ILogger<SendMailConsumer> _logger;
        public SendMailConsumer(ILogger<SendMailConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<EmailModel> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message);
        }
    }
}
