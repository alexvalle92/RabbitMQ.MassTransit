using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Worker.Consumers
{
    class AddCustomerConsumer :
        IConsumer<CustomerModel>
    {
        ILogger<AddCustomerConsumer> _logger;

        public AddCustomerConsumer(ILogger<AddCustomerConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CustomerModel> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message);
        }
    }
}
