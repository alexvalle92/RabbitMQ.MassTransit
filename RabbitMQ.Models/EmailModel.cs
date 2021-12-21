using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
