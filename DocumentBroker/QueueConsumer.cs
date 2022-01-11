using DocumentBroker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentBroker
{
    public class QueueConsumer
    {

        private Validator validator = new Validator();
        private RequestAnalyser requestAnalyser = new RequestAnalyser();
       
        public void Consume(IModel channel)
        {
            //Queue maken en consumen (luisteren of er berichten op de queue komen en bekijken)
            channel.QueueDeclare("queue1",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
            
                //message tonen die op queue binnenkomt
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ////validator.validation("GenerateRequest_validator2.xsd" , message);
                validator.validation("StoreDocument_validator.xsd", message);

                if (Validator.ValidForQ == '1')
                {
                  requestAnalyser.SearchRequest(message, true);
                  Console.WriteLine(message);
                }
                else if (Validator.ValidForQ == '2')
                {
                    requestAnalyser.SearchRequest(message, false);
                    Console.WriteLine(message);
                }

            };
            channel.BasicConsume("queue1", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
