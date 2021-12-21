using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Worker.Consumers
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
