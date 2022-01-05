using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DocumentBroker
{
    public static class QueueConsumer
    {
        

        public static void Consume(IModel channel)
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
                Console.WriteLine(message);
            };
            channel.BasicConsume("queue1", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
