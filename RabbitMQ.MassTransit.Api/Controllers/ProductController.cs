using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.MassTransit.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.MassTransit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IBus _bus;

        public ProductController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            if(product != null)
            {
                Uri uri = new Uri("amqp://localhost/addProductQueue");
                ISendEndpoint sendEndpoint = await _bus.GetSendEndpoint(uri);
                await sendEndpoint.Send(product);

                EmailModel mail = new EmailModel()
                {
                    Content = product.Name,
                    Email = "alex.rvalle1@gmail.com",
                    Subject = "subj"
                };
                Uri uriMail = new Uri("amqp://localhost/sendMailQueue");
                ISendEndpoint sendEndpointMail = await _bus.GetSendEndpoint(uriMail);
                await sendEndpointMail.Send(mail);
                return Ok();
            }

            return BadRequest();
        }
    }
}
