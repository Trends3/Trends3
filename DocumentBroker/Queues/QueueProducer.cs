using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentBroker.Queues
{
     class QueueProducer
    {
        public void ProduceQueues(IModel channel)
        {

            channel.QueueDeclare("Application1_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Application2_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Generate_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Store_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
       
    }
}
