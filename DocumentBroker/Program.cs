using DocumentBroker;
using DocumentBroker.Utils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    public static string URI = "amqp://guest:guest@172.17.0.3:5672";

    static void Main(string[] args)
    {
       
        var factory = new ConnectionFactory
        {
            Uri = new Uri(URI)
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        QueueConsumer.Consume(channel);

        Validator.validation();
    }

}