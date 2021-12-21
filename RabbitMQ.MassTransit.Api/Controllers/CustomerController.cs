using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public CustomerController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerModel customer)
        {
            await _publishEndpoint.Publish<CustomerModel>(customer);
            return Ok();
        }
    }
}
