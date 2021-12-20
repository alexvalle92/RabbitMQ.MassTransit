using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.MassTransit.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Consumers
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
