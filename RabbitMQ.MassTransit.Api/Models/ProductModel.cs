using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
    }
}
