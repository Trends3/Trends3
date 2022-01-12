using DocumentBroker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

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
                char request = requestAnalyser.SearchRequest(message);

                switch (request.ToString())
                {
                    case "g":
                        Console.WriteLine("Generate");
                        validator.validation("GenerateRequest_validator2.xsd", message);
                        break;
                    case "s":
                        Console.WriteLine("Store");
                        validator.validation("StoreDocument_validator.xsd", message);

                        break;
                    case "0":
                        Console.WriteLine("Generate Store");
                        validator.validation("GenerateStoreRequest_validator.xsd", message);

                        break;

                    default:
                        Console.WriteLine("Is invalid");
                        break;
                }
                //validator.validation("StoreDocument_validator.xsd", message);

                if (Validator.ValidForQ == true)
                {
                    Console.WriteLine(message);
                }

            };
            channel.BasicConsume("queue1", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
