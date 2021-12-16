using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.MassTransit.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public ClientesController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente(string nomeCliente)
        {
            await _publishEndpoint.Publish<ICliente>(new
            {
                Value = "Alexandre"
            });
            return Ok();
        }
    }
}
